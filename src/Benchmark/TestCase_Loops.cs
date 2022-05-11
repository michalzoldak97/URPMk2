using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace URPMk2
{
	public class TestCase_Loops : MonoBehaviour, ITestable
	{
		private Dictionary<GameObject, ObjectPoolRefTimePair> objDict = new Dictionary<GameObject, ObjectPoolRefTimePair>();
		private List<ObjectPoolRefTimePair> objList = new List<ObjectPoolRefTimePair>();

        private void InstantiateStores()
        {
            for (int i = 0; i < 1000; i++)
            {
                GameObject testObj = new GameObject();
                ObjectPoolRefTimePair testpair = new ObjectPoolRefTimePair(testObj, 10f);
                objDict.Add(testObj, testpair);
                objList.Add(testpair);
            }
        }
        private void Start()
        {
            InstantiateStores();
        }
        public void RunPrimaryTestCase()
        {
            GameObject testObj = new GameObject();
            if (objDict.ContainsKey(testObj))
                Debug.Log("key");
        }
		public void RunAlternativeTestCase()
        {
            GameObject testObj = new GameObject();
            Parallel.ForEach(objList, objPair =>
            {
                if (objPair.obj == testObj)
                    Debug.Log("It");
            });
        }
	}
}
