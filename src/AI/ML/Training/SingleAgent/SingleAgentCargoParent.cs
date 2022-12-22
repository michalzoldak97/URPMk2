using UnityEngine;

namespace URPMk2
{
	public class SingleAgentCargoParent : MonoBehaviour
	{
		private Transform finalDest;
		private SingleAgentTrainingManager saManager;
		private void SetInit()
		{
			finalDest = GameObject.FindGameObjectWithTag("FinalDest").transform;
			saManager = GetComponent<SingleAgentTrainingManager>();
		}
		
		private void OnEnable()
		{
			SetInit();
			saManager.EventNewAgentSpawned += OnNewAgentSpawned;
		}
		
		private void OnDisable()
		{
            saManager.EventNewAgentSpawned -= OnNewAgentSpawned;
        }
		private void OnNewAgentSpawned(GameObject agent)
		{
			Debug.Log("Cargo prent sets new parent for the new agent");
			agent.GetComponent<ICargoUnit>().SetCargoParent(transform, finalDest);
		}
	}
}
