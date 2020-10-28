using System;
using TapToDefuse.ObjectPool;
using UniRx;
using UnityEngine;

namespace TapToDefuse.Game
{
    public class ParticlePoolObject : BasePoolObject
    {
        private ParticleSystem _particleSystem;
        private float _durationTime;
        private IDisposable _timer;
        public override void OnCreate(string poolTag)
        {
            base.OnCreate(poolTag);
            _particleSystem = GetComponent<ParticleSystem>();
            _durationTime = _particleSystem.main.duration;
        }

        public override void OnSpawn(object additionalSettings = null)
        {
            _particleSystem.transform.localScale = Vector3.one;
            
            _particleSystem.Play();
            _timer = Observable.Timer(TimeSpan.FromSeconds(_durationTime)).Subscribe(_ =>
            {
                ObjectPooler.Instance.ReturnToPool(this);
            });
        }

        private void StopTimer()
        {
            if (_timer == null) 
                return;
            
            _timer.Dispose();
            _timer = null;
        }

        public override void OnReturn()
        {
            StopTimer();
        }
    }
}