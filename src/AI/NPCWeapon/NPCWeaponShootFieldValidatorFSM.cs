using UnityEngine;

namespace URPMk2
{
	public class NPCWeaponShootFieldValidatorFSM : NPCWeaponShootFieldValidator
	{
        protected override void Start()
        {
            FSMStateManager fManager = GetComponent<FSMStateManager>();
            attackRange = fManager.GetFSMSettings().attackRange;
            shiftRange = fManager.GetFSMSettings().shiftRange;
            friendlyLayers = fManager.GetFSMSettings().friendlyLayers;
            navMeshAgent = fManager.MyNavMeshAgent;
            stopDist = navMeshAgent.stoppingDistance;
        }
    }
}
