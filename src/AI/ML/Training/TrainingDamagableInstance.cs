using UnityEngine;

namespace URPMk2
{
	public class TrainingDamagableInstance : MonoBehaviour
	{
		[SerializeField] private TrainingUnitManager tuManager;
		private DamagableMaster dmgMaster;
		private void SetInit()
		{
			dmgMaster = GetComponent<DamagableMaster>();
        }
		
		private void OnEnable()
		{
			SetInit();
			dmgMaster.EventDestroyObject += OnInstanceDestroyed;
        }
		
		private void OnDisable()
		{
            dmgMaster.EventDestroyObject -= OnInstanceDestroyed;
        }
		private void OnInstanceDestroyed(Transform killer)
		{
			tuManager.CallEventAgentDestroyed(killer, transform);
		}
	}
}
