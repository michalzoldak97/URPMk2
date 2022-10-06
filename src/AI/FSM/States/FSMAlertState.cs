using UnityEngine;

namespace URPMk2
{
	public class FSMAlertState : IFSMState
	{
        private int targetDetections, lastDetectionCnt;
        private readonly FSMStateManager fManager;

        public FSMAlertState(FSMStateManager fManager)
        {
            this.fManager = fManager;
        }
        private void GoToLocationOfInterest()
        {
            if (!fManager.MyNavMeshAgent.enabled || 
                fManager.LocationOfInterest == Vector3.zero)
                return;

            fManager.MyNavMeshAgent.SetDestination(fManager.LocationOfInterest);
            fManager.MyNavMeshAgent.isStopped = false;

            if (targetDetections != 0 ||
                fManager.MyNavMeshAgent.pathPending ||
                fManager.MyNavMeshAgent.remainingDistance >= fManager.MyNavMeshAgent.stoppingDistance)
                return;

            fManager.LocationOfInterest = Vector3.zero; // no target found at the destination
            fManager.currentState = fManager.patrolState;
        }
        private void Look()
        {
            lastDetectionCnt = targetDetections;

            FSMTarget target = fManager.IsTargetVisible();
            if (target.isVisible)
                targetDetections++;

            if (lastDetectionCnt == targetDetections)
                targetDetections = 0;
            else if (targetDetections >= fManager.GetFSMSettings().requiredDetectionCount)
            {
                targetDetections = 0;
                fManager.LocationOfInterest = target.targetTransform.position;
                fManager.PursueTarget = target.targetTransform;
                fManager.AlertAllies();
                fManager.currentState = fManager.pursueState;
                return;
            }

            GoToLocationOfInterest();
        }
        public void UpdateState()
        {
            Look();
        }
    }
}
