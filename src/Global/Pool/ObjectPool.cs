using System;
using System.Collections;
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
    public class PoolInstance
    {
        public bool IsLocked { get; private set; }
        public int ObjIdx { get; set; }
        public string poolTag;
        public GameObject[] objects;

        public bool HasFreeObjects()
        {
            return ObjIdx < objects.Length;
        }
        public GameObject GetObject()
        {
            ObjIdx++;
            return objects[ObjIdx - 1];
        }
        public void Lock()
        {
            IsLocked = true;
        }
        public void Unlock()
        {
            ObjIdx = 0;
            IsLocked = false;
        }
        public void SupplementObj(GameObject obj)
        {
            objects[ObjIdx] = obj;
        }
    }
    // contains pools of objects 'pools'
    // each pool will return unused object (using current index 'objIdx')
    // if pool is used at given frame its index will be set to 0 at the end
    // if all objects are used 'maxAdditional' will be instantiated
    // object should start deactivating itself on enable

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
                objPool.objects = new GameObject[pool.size];

                for (int i = 0; i < pool.size; i++)
                {
                    GameObject obj = Instantiate(pool.prefab);
                    obj.SetActive(false);

                    objPool.objects[i] = obj;
                }

                objectPools.Add(pool.tag, objPool);
            }
        }
        private void Start()
        {
            BuildPools();
            objMonitor = gameObject.AddComponent<ObjectPoolMonitor>();
        }
        private IEnumerator UnlockPoolInstance(PoolInstance instance)
        {
            yield return new WaitForEndOfFrame();
            instance.Unlock();
        }
        public GameObject GetObjectFromPool(string tag)
        {
            if (!objectPools.ContainsKey(tag))
                return null;

            if (objectPools[tag].HasFreeObjects())
            {
                GameObject obj = objectPools[tag].GetObject();
                if (obj == null)
                {
                    obj = Instantiate(GetPoolByTag(tag).prefab);
                    objectPools[tag].SupplementObj(obj);
                }
                objMonitor.RegisterInMonitor(obj);
                return obj;
            }

            if (!objectPools[tag].IsLocked)
            {
                objectPools[tag].Lock();
                StartCoroutine(UnlockPoolInstance(objectPools[tag]));
            }
            return null;
        }
    }
}
