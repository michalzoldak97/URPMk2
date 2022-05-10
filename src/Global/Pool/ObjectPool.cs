using System;
using System.Collections.Generic;
using UnityEngine;

namespace URPMk2
{
    // class used to create pools at Start
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size, maxAdditional;
    }
    // contains pools of objects 'pools'
    // each pool instance will return unused object (using current index 'objIdx')
    // if pool is used at given frame its index will be set to 0 at the end
    // if all objects are used 'maxAdditional' will be instantiated

    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private Pool[] pools;
        private ObjectPoolMonitor objMonitor;
        private Pool GetPoolByTag(string tag)
        {
            for (int i = 0; i < pools.Length; i++)
            {
                if (String.Equals(pools[i].tag, tag))
                    return pools[i];
            }
            return null;
        }
        private Dictionary<string, PoolInstance> objectPools; 

        private void BuildPools()
        {
            objectPools = new Dictionary<string, PoolInstance>();
            foreach (Pool pool in pools)
            {
                PoolInstance objPool = new PoolInstance();
                objPool.objects = new PooledObjectInstance[pool.size];

                for (int i = 0; i < pool.size; i++)
                {
                    GameObject gObj = Instantiate(pool.prefab);
                    gObj.SetActive(false);

                    objPool.objects[i] = new PooledObjectInstance
                    {
                        obj = gObj,
                        objBehavior = gObj.GetComponent<IPooledObject>()
                    };
                }

                objectPools.Add(pool.tag, objPool);
            }
        }
        private void Start()
        {
            BuildPools();
            objMonitor = gameObject.AddComponent<ObjectPoolMonitor>();
        }
        public PooledObjectInstance GetObjectFromPool(string tag)
        {
            if (!objectPools.ContainsKey(tag))
                return null;

            if (objectPools[tag].HasFreeObjects())
            {
                PooledObjectInstance obj = objectPools[tag].GetObject();
                if (obj == null)
                {
                    GameObject toSup = Instantiate(GetPoolByTag(tag).prefab);
                    obj = new PooledObjectInstance
                    {
                        obj = toSup,
                        objBehavior = toSup.GetComponent<IPooledObject>()
                    };
                    objectPools[tag].SupplementObj(obj);
                }
                objMonitor.RegisterInMonitor(obj.obj);
                return obj;
            }
            return null;
        }
    }
}
