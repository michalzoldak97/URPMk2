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
    public class PoolInstance : MonoBehaviour
    {
        public bool isLocked { get; private set; }
        public int objIdx { get; set; }
        public string poolTag;
        public GameObject[] objects;

        public bool HasFreeObjects()
        {
            return objIdx < objects.Length - 1;
        }
        public GameObject GetObject()
        {
            objIdx++;
            return objects[objIdx - 1];
        }
        private IEnumerator UnlockPool()
        {
            yield return new WaitForEndOfFrame();
            objIdx = 0;
            isLocked = false;
        }
        public void Lock()
        {
            isLocked = true;
            StartCoroutine(UnlockPool());
        }
        public void SupplementObj(GameObject obj)
        {
            objects[objIdx] = obj;
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
                return obj;
            }

            if (!objectPools[tag].isLocked)
                objectPools[tag].Lock();

            return null;
        }
    }
}
