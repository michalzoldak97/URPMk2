using UnityEngine;
using URPMk2;

namespace SD
{
	public class SquadCargoDamage : MonoBehaviour
	{
        private Vector3 target;

        private SquadCargoMaster cargoMaster;
		private DamagableMaster dmgMaster;
		private void SetInit()
		{
            target = GameObject.FindGameObjectWithTag("FinalDest").transform.position;

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
        private void EvaluateDestruction()
        {
            if (Vector3.Distance(transform.position, target) > 30f)
                cargoMaster.OnCargoDestroyed();
            else
                cargoMaster.OnTargetReached();
        }
        private void OnCargoDestroy(Transform t)
		{
			EvaluateDestruction();
            cargoMaster.CallEventCargoDestroyed(0f);
		}
	}
}
