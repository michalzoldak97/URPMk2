using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

namespace URPMk2
{
	// keeps track of all objects given from pool
	// list holds obj references and time left to unparent & deactivate
	// if any obj is retrieved from any pool objects are added to dict
	// if any obj is in dict it will periodically check if it should be released
	public class ObjectPoolRefTimePair
    {
		public GameObject obj;
		public float time;

		public ObjectPoolRefTimePair(GameObject obj, float time)
        {
			this.obj = obj;
			this.time = time;
        }
	}
	public class ObjectPoolMonitor : MonoBehaviour
	{
		private bool isUpdateOn;
		private float decreaseRate;
		private List<ObjectPoolRefTimePair> issuedObjects;
		private WaitForSeconds waitForNextUpdate;

        private void Start()
        {
			decreaseRate = GameConfig.secToCheckForPoolObj;
			issuedObjects = new List<ObjectPoolRefTimePair>();
			waitForNextUpdate = new WaitForSeconds(GameConfig.secToCheckForPoolObj);
		}

		private bool SetTimeIfObjExists(GameObject obj)
        {
			if (issuedObjects.Count < 1)
				return false;
			bool hasObj = false;
			Parallel.ForEach(issuedObjects, objTime =>
			{
				if (objTime.obj == obj)
				{
					objTime.time = GameConfig.secEffectAlive;
					hasObj = true;
				}
			});
			return hasObj;
        }
		private IEnumerator ResetPooledObject(GameObject obj)
        {
			yield return new WaitForEndOfFrame();
			obj.transform.SetParent(null);
		}
		private IEnumerator UpdateObjects()
        {
			isUpdateOn = true;
			while (issuedObjects.Count > 0)
            {
				for (int i = 0; i < issuedObjects.Count; i++)
                {
					issuedObjects[i].time -= decreaseRate;
					if (issuedObjects[i].time < 0)
					{
						issuedObjects[i].obj.SetActive(false);
						StartCoroutine(ResetPooledObject(issuedObjects[i].obj));
						issuedObjects.Remove(issuedObjects[i]);
					}
				}
				yield return waitForNextUpdate;
            }
			isUpdateOn = false;
		}
		public void RegisterInMonitor(GameObject obj)
        {
			if (SetTimeIfObjExists(obj))
				return;

			issuedObjects.Add(
				new ObjectPoolRefTimePair(obj, GameConfig.secEffectAlive));

			if (!isUpdateOn)
				StartCoroutine(UpdateObjects());
        }
	}
}
