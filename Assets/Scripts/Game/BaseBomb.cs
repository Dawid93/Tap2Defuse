using System;
using System.Collections;
using System.Collections.Generic;
using TapToDefuse.Helpers;
using TapToDefuse.ObjectPool;
using UniRx;
using UniRx.InternalUtil;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TapToDefuse.Game
{
    public abstract class BaseBomb : BasePoolObject, IPointerClickHandler
    {
        [SerializeField] protected Image counterSprite;
        [SerializeField] private float scaleTime = 0.1f;

        protected float TimerValue;
        
        private RectTransform _rect;
        private IDisposable _counter;
        private BombCell _currentCell;
        

        public override void OnCreate(string poolTag)
        {
            base.OnCreate(poolTag);
            _rect = GetComponent<RectTransform>();
        }

        public override void OnSpawn(object additionalSettings)
        {
            GameManager.Instance.OnFinishGame += StopCounter;
            if (additionalSettings is TapBombSettings tbs)
            {
                _currentCell = tbs.BombCell;
                _currentCell.SetBomb(this);
            }
            counterSprite.fillAmount = 0;
            _rect.localScale = Vector3.zero;
            LeanTween.scale(_rect, Vector3.one, scaleTime).setOnComplete(StartCounter);
        }

        public override void OnReturn()
        {
            ObjectPooler.Instance.GetFromPool(PoolTagHelper.DefuseParticleTag, transform.position,
                Quaternion.identity, null);
            _currentCell.SetBomb(null);
            _currentCell = null;
            StopCounter();
            transform.localScale = Vector3.zero;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnTapAction();
        }

        protected virtual void OnTapAction()
        {
            ObjectPooler.Instance.ReturnToPool(this);
        }

        protected virtual void OnTimeEnd()
        {
        }

        private void StartCounter()
        {
            _counter = Observable.FromMicroCoroutine(MicroCoroutine).Subscribe();
        }

        private IEnumerator MicroCoroutine()
        {
            float currentTime = 0;
            while (true)
            {
                currentTime += Time.deltaTime;
                counterSprite.fillAmount = Mathf.Clamp01(currentTime / TimerValue);
                if(currentTime >= TimerValue)
                    OnTimeEnd();
                yield return null;
            }
        }

        private void StopCounter()
        {
            if (_counter == null) 
                return;
            
            _counter.Dispose();
            _counter = null;
        }
    }
}
