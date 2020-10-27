using System.Collections.Generic;
using UnityEngine;

namespace TapToDefuse.ObjectPool
{
    public class ObjectPooler : MonoBehaviour
    {
        public static ObjectPooler Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<ObjectPooler>();
                    DontDestroyOnLoad(_instance.gameObject);
                }

                return _instance;
            }
        }
        private static ObjectPooler _instance;
        
        [SerializeField] private Vector3 spawnPos;
        [SerializeField] private PoolContainer poolContainer;
        [SerializeField] private Transform startPoolParent;

        private Dictionary<string, Queue<BasePoolObject>> _poolDict;
        private bool _poolIsInit;
        private void Awake()
        {
            if (_instance == null)
                _instance = this;
            
            InitPools();
        }

        private void InitPools()
        {
            if (!poolContainer)
            {
                Debug.LogError("[ObjectPooler] PoolContainer is missing");
                return;
            }
            if(_poolIsInit || !poolContainer)
                return;

            _poolDict = new Dictionary<string, Queue<BasePoolObject>>();

            foreach (var pool in poolContainer.Pools)
            {
                Queue<BasePoolObject> poolObjectsQ = new Queue<BasePoolObject>();
                
                for (int i = 0; i < pool.PoolSize; i++)
                {
                    BasePoolObject bpo = Instantiate(pool.PoolObject, spawnPos, Quaternion.identity, startPoolParent);
                    bpo.OnCreate(pool.PoolTag);
                    bpo.gameObject.SetActive(false);
                    DontDestroyOnLoad(bpo.gameObject);
                    poolObjectsQ.Enqueue(bpo);
                }
                _poolDict.Add(pool.PoolTag, poolObjectsQ);
            }

            _poolIsInit = true;
        }

        public BasePoolObject GetFromPool(string poolTag, Vector3 pos, Quaternion rot, Transform parent)
        {
            if (!_poolIsInit)
                InitPools();
            if (_poolDict == null)
                return null;
            if (!_poolDict.ContainsKey(poolTag))
                return null;

            BasePoolObject poolObject = _poolDict[poolTag].Dequeue();
            Transform poolTransform = poolObject.transform;
            
            poolTransform.SetParent(parent);
            poolTransform.localPosition = pos;
            poolTransform.localRotation = rot;
            poolObject.gameObject.SetActive(true);
            poolObject.OnSpawn();
            
            _poolDict[poolTag].Enqueue(poolObject);
            return poolObject;
        }

        public void ReturnToPool(BasePoolObject basePoolObject)
        {
            basePoolObject.OnReturn();
            Transform poolTransform = basePoolObject.transform;
            poolTransform.SetParent(null);
            poolTransform.position = spawnPos;
            basePoolObject.gameObject.SetActive(false);
        }
    }
}
