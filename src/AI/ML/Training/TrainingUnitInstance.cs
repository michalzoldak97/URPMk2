using UnityEngine;

namespace URPMk2
{
	public class TrainingUnitInstance : MonoBehaviour
	{
		private TrainingUnitManager tuManager;
		private DamagableMaster dmgMaster;
        public void SetTrainingUnitManager(TrainingUnitManager tuManager)
		{
			this.tuManager = tuManager;
		}
		private void SetInit()
		{
			dmgMaster = GetComponent<DamagableMaster>();
        }
		
		private void OnEnable()
		{
			SetInit();
			dmgMaster.EventReceivedDamage += OnObjectDamage;
			dmgMaster.EventDestroyObject += OnObjectDestroy;
        }
		
		private void OnDisable()
		{
            dmgMaster.EventReceivedDamage -= OnObjectDamage;
            dmgMaster.EventDestroyObject -= OnObjectDestroy;
        }
		private void OnObjectDamage(Transform origin, float dmg)
		{
			tuManager.CallEventAgentDamaged(origin, transform, dmg);
		}
		private void OnObjectDestroy(Transform killer)
		{
			tuManager.CallEventAgentDestroyed(killer, transform);
        }
	}
}
