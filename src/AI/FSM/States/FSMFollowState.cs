using UnityEngine;

namespace URPMk2
{
	public class FSMFollowState : IFSMState
    {
        private readonly FSMStateManager fManager;
        public FSMFollowState(FSMStateManager fManager)
        {
            this.fManager = fManager;
        }
        private void Look()
        {
            FSMTarget enemy = fManager.IsTargetVisible();
            if (enemy.isVisible)
            {
                fManager.LocationOfInterest = enemy.targetTransform.position;
                fManager.currentState = fManager.alertState;
                return;
            }
        }
        private void FollowTarget()
        {
            if (!fManager.MyNavMeshAgent.enabled)
                return;

            if (fManager.MyFollowTarget == null)
            {
                fManager.currentState = fManager.patrolState;
                return;
            }

            fManager.MyNavMeshAgent.SetDestination(fManager.MyFollowTarget.position);
            fManager.MyNavMeshAgent.isStopped = false;

            fManager.CheckIfFollowDestinationReached();
        }
        public void UpdateState()
        {
            Look();
            FollowTarget();
        }
    }
}
