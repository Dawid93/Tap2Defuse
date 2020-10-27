using TapToDefuse.ObjectPool;
using UnityEngine;

namespace TapToDefuse.Game
{
    public class AvoidBomb : BaseBomb
    {
        [SerializeField] private float timerValue = 3;

        public override void OnSpawn(object additionalSettings)
        {
            TimerValue = timerValue;
            base.OnSpawn(additionalSettings);
        }

        protected override void OnTimeEnd()
        {
            base.OnTimeEnd();
            ObjectPooler.Instance.ReturnToPool(this);
        }
    }
}