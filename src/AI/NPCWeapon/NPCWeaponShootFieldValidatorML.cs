using UnityEngine.AI;

namespace URPMk2
{
	public class NPCWeaponShootFieldValidatorML : NPCWeaponShootFieldValidator
    {
        protected override void Start()
        {
            base.Start();
            IStateManager fManager = GetComponent<IStateManager>();
            attackRange = fManager.GetFSMSettings().attackRange;
            shiftRange = fManager.GetFSMSettings().shiftRange;
            friendlyLayers = fManager.GetFSMSettings().friendlyLayers;
            navMeshAgent = GetComponent<NavMeshAgent>();
            stopDist = navMeshAgent.stoppingDistance;
        }
    }
}
