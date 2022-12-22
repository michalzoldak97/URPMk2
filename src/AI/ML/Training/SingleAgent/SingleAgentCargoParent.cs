using UnityEngine;

namespace URPMk2
{
	public class SingleAgentCargoParent : MonoBehaviour
	{
		[SerializeField] Transform finalDest;
		private SingleAgentTrainingManager saManager;
		private void SetInit()
		{
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
			agent.GetComponent<ICargoUnit>().SetCargoParent(transform, finalDest);
		}
	}
}
