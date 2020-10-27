using TapToDefuse.Interface;
using TapToDefuse.ObjectPool;

namespace TapToDefuse.Game
{
    public abstract class BaseBomb : BasePoolObject, ITapable
    {
        public override void OnSpawn()
        {
            throw new System.NotImplementedException();
        }

        public override void OnReturn()
        {
            throw new System.NotImplementedException();
        }

        public void OnTap()
        {
            throw new System.NotImplementedException();
        }
    }
}
