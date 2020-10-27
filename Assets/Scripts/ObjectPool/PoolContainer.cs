using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace TapToDefuse.ObjectPool
{
    [CreateAssetMenu(fileName = "PoolContainer", menuName = "ObjectPool/PoolContainer")]
    public class PoolContainer : ScriptableObject
    {
        public List<Pool> Pools => pools;
        
        [SerializeField, ReorderableList] private List<Pool> pools;
    }

    [Serializable]
    public class Pool
    {
        public string PoolTag => poolTag;
        public BasePoolObject PoolObject => poolObject;
        public int PoolSize => poolSize;

        [SerializeField] private string poolTag;
        [SerializeField] private BasePoolObject poolObject;
        [SerializeField] private int poolSize;
    }
}
