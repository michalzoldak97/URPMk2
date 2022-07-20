using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/**/

namespace URPMk2
{
    public class FSMPatrolState : IFSMState
	{
        private int sightRangePow, nextWayPoint;
        private Teams[] teamsToAttack;
        private Vector3 heading;
        private Transform fTransform;
        private FSMStateManager fManager;

        public FSMPatrolState (FSMStateManager fManager)
        {
            this.fManager = fManager;
            fTransform = fManager.transform;
            sightRangePow = fManager.GetFSMSettings().sightRange * 
                fManager.GetFSMSettings().sightRange;
            teamsToAttack = fManager.GetFSMSettings().teamsToAttack;
        }
        public void ToAlertState()
        {
            fManager.currentState = fManager.alertState;
        }
        private void SetUpAlertState(Transform target)
        {
            fManager.LocationOfInterest = target.position;
            ToAlertState();
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
        private bool RandomWanderTarget(Vector3 centre)
        {
            Vector3 noAbsRnd = Random.insideUnitSphere * fManager.GetFSMSettings().sightRange;
            Debug.Log("Random Point is: " + noAbsRnd);
            Vector3 rndPoint = centre + Utils.GetAbsVector3(noAbsRnd);

            // find nearest plane
            if (Physics.Raycast(rndPoint, -rndPoint.y * Vector3.up, out RaycastHit hit))
            {
                rndPoint = hit.transform.position;
            }

            // TO DO global function searching on terrarain

            Debug.Log("Point is: " + rndPoint);

            if(NavMesh.SamplePosition(rndPoint, out NavMeshHit navHit, GameConfig.wanderTargetRandomRadius, NavMesh.AllAreas))
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
        private float CalculateDotProd(Transform target)
        {
            heading = (target.position - fTransform.position).normalized;
            return Vector3.Dot(heading, fTransform.forward);
        }
        private void Look()
        {
            List<ITeamMember> enemiesInRange = TeamMembersManager.GetTeamMembersInRange(
                    teamsToAttack,
                    fTransform.position,
                    sightRangePow
                );

            int numEnemies = enemiesInRange.Count;
            for (int i = 0; i < numEnemies; i++)
            {
                if (CalculateDotProd(enemiesInRange[i].ObjTransform) < fManager.GetFSMSettings().minDotProd)
                    continue;

                if (VisibilityCalculator.IsVisibleSingle(fManager.VisibilityParams, heading, enemiesInRange[i].ObjTransform)
                    || (heading.sqrMagnitude < fManager.VisibilityParams.highResSearchSqrRange &&
                    VisibilityCalculator.IsVisibleCorners(fManager.VisibilityParams, enemiesInRange[i].ObjTransform, enemiesInRange[i].BoundsExtens)))
                {
                    Debug.Log("Found  " + enemiesInRange[i].ObjTransform.name);
                    //SetUpAlertState(enemiesInRange[i].ObjTransform);
                }
            }
        }
        private void Patrol()
        {
            if (fManager.MyFollowTarget != null)
                fManager.currentState = fManager.followState;

            if (!fManager.MyNavMeshAgent.enabled)
                return;

            if(fManager.waypoints.Length > 0)
            {
                MoveToTarget(fManager.waypoints[nextWayPoint].position);

                if (IsDestinationReached())
                {
                    nextWayPoint = (nextWayPoint + 1) % fManager.waypoints.Length;
                }
            }
            else
            {
                Debug.Log("is destination reached: " + IsDestinationReached());
                if (IsDestinationReached())
                {
                    fManager.MyNavMeshAgent.isStopped = true;

                    Debug.Log("is RandomWanderTarget: " + RandomWanderTarget(fTransform.position));
                    if (RandomWanderTarget(fTransform.position))
                        MoveToTarget(fManager.WanderTarget);
                }
            }
        }
		public void UpdateState()
        {
            Look();
            Patrol();
        }
		public void ToPatrolState()
        {

        }
		public void ToPursueState()
        {

        }
		public void ToAttackState()
        {

        }
	}
}
