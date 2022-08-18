using UnityEngine;

namespace URPMk2
{
	public class FSMAlertState : IFSMState
	{
        private int targetDetections, lastDetectionCnt;
        private FSMStateManager fManager;

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

            if (fManager.MyNavMeshAgent.remainingDistance >= fManager.MyNavMeshAgent.stoppingDistance ||
                fManager.MyNavMeshAgent.pathPending)
                return;

            fManager.LocationOfInterest = Vector3.zero; // no target found at the destination
            ToPatrolState();
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
                // ToPursueState();
            }

            GoToLocationOfInterest();
        }
        public void UpdateState()
        {
            Look();
        }
        public void ToPatrolState()
        {
            fManager.currentState = fManager.patrolState;
        }
        public void ToAlertState()
        {

        }
        public void ToPursueState()
        {
            fManager.currentState = fManager.pursueState;
        }
        public void ToAttackState()
        {

        }
    }
}
