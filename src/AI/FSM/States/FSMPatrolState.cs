using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace URPMk2
{
    public class FSMPatrolState : IFSMState
	{
        private int nextWayPoint;
        private readonly Transform fTransform;
        private readonly FSMStateManager fManager;

        public FSMPatrolState (FSMStateManager fManager)
        {
            this.fManager = fManager;
            fTransform = fManager.transform;
        }
        private void SetUpAlertState(Transform target)
        {
            fManager.LocationOfInterest = target.position;
            fManager.currentState = fManager.alertState;
        }
        private bool IsDestinationReached()
        {
            if (fManager.MyNavMeshAgent.remainingDistance <= fManager.MyNavMeshAgent.stoppingDistance &&
                !fManager.MyNavMeshAgent.pathPending)
            {
                fManager.MyNavMeshAgent.isStopped = true;
                return true;
            }
            else
            {
                fManager.MyNavMeshAgent.isStopped = false;
                return false;
            }
        }
        private void MoveToTarget(Vector3 target)
        {
            if (Vector3.Distance(fTransform.position, target) > fManager.MyNavMeshAgent.stoppingDistance + 1)
            {
                fManager.MyNavMeshAgent.SetDestination(target);
                fManager.MyNavMeshAgent.isStopped = false;
            }
        }
        private bool ExistsRandomWanderTarget(Vector3 centre)
        {
            float randRange = fManager.GetFSMSettings().sightRange;
            Vector3 rndPoint = fTransform.position;
            rndPoint.x += Random.Range(-rndPoint.x, randRange);
            rndPoint.y += Random.Range(-rndPoint.y, randRange);
            
            // find nearest plane
            if (Physics.Raycast(rndPoint, -Vector3.up * rndPoint.y, out RaycastHit hit, fManager.GetFSMSettings().sightRange))
                rndPoint = hit.point;

            if (NavMesh.SamplePosition(rndPoint, out NavMeshHit navHit, GameConfig.wanderTargetRandomRadius, NavMesh.AllAreas))
            {
                fManager.WanderTarget = navHit.position;
                return true;
            }
            else
            {
                fManager.WanderTarget = centre;
                return false;
            }
        }
        private void Look()
        {
            FSMTarget target = fManager.MyNPCMaster.NpcLook.IsTargetVisible();
            if (target.isVisible)
                SetUpAlertState(target.targetTransform);
        }
        private void Patrol()
        {
            if (fManager.MyFollowTarget != null)
                fManager.currentState = fManager.followState;

            if (!fManager.MyNavMeshAgent.enabled ||
                fManager.currentState == fManager.alertState)
                return;

            if (fManager.waypoints.Length > 0)
            {
                MoveToTarget(fManager.waypoints[nextWayPoint].position);

                if (IsDestinationReached())
                    nextWayPoint = (nextWayPoint + 1) % fManager.waypoints.Length;
            }
            else
            {
                // Debug.Log("is destination reached: " + IsDestinationReached());
                if (IsDestinationReached())
                {
                    fManager.MyNavMeshAgent.isStopped = true;

                    // Debug.Log("is RandomWanderTarget: " + RandomWanderTarget(fTransform.position));
                    if (ExistsRandomWanderTarget(fTransform.position))
                        MoveToTarget(fManager.WanderTarget);
                }
            }
        }
		public void UpdateState()
        {
            Look();
            Patrol();
        }
	}
}
