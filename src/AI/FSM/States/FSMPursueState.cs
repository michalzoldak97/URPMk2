using UnityEngine;

namespace URPMk2
{
	public class FSMPursueState : IFSMState
	{
        private Transform fTransform;
        private FSMStateManager fManager;
        public FSMPursueState(FSMStateManager fManager)
        {
            this.fManager = fManager;
            fTransform = fManager.transform;
        }
        private void Look()
        {
            if (fManager.PursueTarget == null)
            {
                ToPatrolState();
                return;
            }

            ITeamMember[] enemiesInRange = fManager.GetEnemiesInRange();
            if (enemiesInRange.Length <= 0)
            {
                fManager.PursueTarget = null;
                ToPatrolState();
                return;
            }

            float minDist = fManager.SightRangePow * 2;
            float distToEnemy;

            int eirLen = enemiesInRange.Length;
            for (int i = 0; i < eirLen; i++)
            {
                if (enemiesInRange[i] == null)
                    break;

                distToEnemy = (enemiesInRange[i].ObjTransform.position - fTransform.position).sqrMagnitude;

                if (distToEnemy < minDist)
                {
                    minDist = distToEnemy;
                    fManager.PursueTarget = enemiesInRange[i].ObjTransform;
                }
            }

        }
        private void Pursue()
        {
            if (!fManager.MyNavMeshAgent.enabled ||
                fManager.PursueTarget == null)
            {
                ToAlertState();
                return;
            }
            fManager.MyNavMeshAgent.SetDestination(fManager.PursueTarget.position);
            fManager.LocationOfInterest = fManager.PursueTarget.position;
            fManager.MyNavMeshAgent.isStopped = false;

            float distToEnemy = (fManager.PursueTarget.position - fTransform.position).sqrMagnitude;

            if (distToEnemy <= fManager.GetFSMSettings().attackRange)
                ToAttackState();

        }
        public void UpdateState()
        {
            Look();
            Pursue();
        }
        public void ToPatrolState()
        {
            fManager.MyNavMeshAgent.isStopped = false;
            fManager.currentState = fManager.patrolState;
        }
        public void ToAlertState()
        {
            fManager.MyNavMeshAgent.isStopped = false;
            fManager.currentState = fManager.alertState;
        }
        public void ToPursueState()
        {

        }
        public void ToAttackState()
        {
            Debug.Log("Pursuing the target ");
            // fManager.currentState = fManager.attackState;
        }
    }
}
