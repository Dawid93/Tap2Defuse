using UnityEngine;

namespace TapToDefuse.Game
{
    public class TapBomb : BaseBomb
    {
        public override void OnSpawn(object additionalSettings)
        {
            if (additionalSettings is TapBombSettings tbs)
            {
                TimerValue = tbs.TimeToExplode;
            }
            base.OnSpawn(additionalSettings);
        }

        protected override void OnTapAction()
        {
            base.OnTapAction();
            GameStats.Instance.DefusedBombs += 1;
        }
    }
}