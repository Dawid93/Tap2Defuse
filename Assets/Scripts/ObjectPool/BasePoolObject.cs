using UnityEngine;

namespace TapToDefuse.ObjectPool
{
    public abstract class BasePoolObject : MonoBehaviour
    {
        public string PoolTag { get; private set; }

        public virtual void OnCreate(string poolTag)
        {
            PoolTag = poolTag;
        }

        public abstract void OnSpawn(object additionalSettings = null);
        public abstract void OnReturn();
    }
}
