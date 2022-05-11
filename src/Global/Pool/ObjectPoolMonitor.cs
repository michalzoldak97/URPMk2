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
		private Dictionary<GameObject, ObjectPoolRefTimePair> issuedObjects;
		private WaitForSeconds waitForNextUpdate;

        private void Start()
        {
			decreaseRate = GameConfig.secToCheckForPoolObj;
			issuedObjects = new Dictionary<GameObject, ObjectPoolRefTimePair>();
			waitForNextUpdate = new WaitForSeconds(GameConfig.secToCheckForPoolObj);
		}

		private bool SetTimeIfObjExists(GameObject obj)
        {
			if (issuedObjects.Count < 1 || !issuedObjects.ContainsKey(obj))
				return false;

			issuedObjects[obj].time = GameConfig.secEffectAlive;

			return true;
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
				List<GameObject> ks = new List<GameObject>(issuedObjects.Keys);
				foreach (GameObject k in ks)
                {
					issuedObjects[k].time -= decreaseRate;
					if (issuedObjects[k].time < 0)
					{
						GameObject o = issuedObjects[k].obj;
						o.SetActive(false);
						StartCoroutine(ResetPooledObject(o));
						issuedObjects.Remove(o);
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
				obj, new ObjectPoolRefTimePair(obj, GameConfig.secEffectAlive));

			if (!isUpdateOn)
				StartCoroutine(UpdateObjects());
        }
	}
}
