using System;
using NaughtyAttributes;
using UnityEngine;

namespace TapToDefuse.Game
{
    public class BombCell : MonoBehaviour
    {
        public bool IsAvailable => _bomb == null;
        private BaseBomb _bomb;
        public RectTransform RectTransform
        {
            get
            {
                if (_rect == null)
                    _rect = GetComponent<RectTransform>();
                return _rect;
            }
        }
        [SerializeField] private float bombRadius = 50;

        private RectTransform _rect;

        public void SetBomb(BaseBomb bomb)
        {
            _bomb = bomb;
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, bombRadius);
        }
    }
}
