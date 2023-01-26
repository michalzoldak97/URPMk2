using UnityEngine;
using URPMk2;

namespace SD
{
	public class SquadCargoDamage : MonoBehaviour, IFinalDestinationEnjoyer
	{
        private SquadCargoMaster cargoMaster;
		private DamagableMaster dmgMaster;

		private void SetInit()
		{
            cargoMaster = GetComponent<SquadCargoMaster>();
			dmgMaster = GetComponent<DamagableMaster>();
		}
		
		private void OnEnable()
		{
			SetInit();
			dmgMaster.EventReceivedDamage += OnCargoDamage;
			dmgMaster.EventDestroyObject += OnCargoDestroy;
		}
		
		private void OnDisable()
		{
			dmgMaster.EventReceivedDamage -= OnCargoDamage;
            dmgMaster.EventDestroyObject -= OnCargoDestroy;
        }
		private void OnCargoDamage(Transform t, float dmg)
		{
			cargoMaster.CallEventCargoDamaged(dmg);
		}
        private void OnCargoDestroy(Transform t)
		{
            cargoMaster.OnCargoDestroyed();
            cargoMaster.CallEventCargoDestroyed(0f);
		}

		public void FinalDestinationReached()
		{
			cargoMaster.OnTargetReached();
		}
	}
}
