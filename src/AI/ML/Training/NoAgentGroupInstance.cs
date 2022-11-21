using UnityEngine;

namespace URPMk2
{
	public class NoAgentGroupInstance : MonoBehaviour
	{
		private TrainingNoAgentGroupSpawner gManager;
		private DamagableMaster dmgMaster;

        public void SetTrainingNoAgentGroupSpawner(TrainingNoAgentGroupSpawner gManager)
		{
			this.gManager = gManager;
        }
		private void SetInit()
		{
			dmgMaster = GetComponent<DamagableMaster>();
        }
		
		private void OnEnable()
		{
			SetInit();
			dmgMaster.EventDestroyObject += OnAgentDestroyed;
		}
		
		private void OnDisable()
		{
            dmgMaster.EventDestroyObject -= OnAgentDestroyed;
        }
		private void OnAgentDestroyed(Transform killer)
		{
			gManager.OnAgentDestroyed(killer);
        }
	}
}
