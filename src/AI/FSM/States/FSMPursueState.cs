using UnityEngine;

namespace URPMk2
{
	public class FSMPursueState : IFSMState
	{
        private int notFoundCounterVal;
        private readonly int notFoundCounter;
        private readonly float attackRangePow;
        private readonly Transform fTransform;
        private readonly FSMStateManager fManager;
        public FSMPursueState(FSMStateManager fManager)
        {
            this.fManager = fManager;
            fTransform = fManager.transform;
            notFoundCounter = fManager.GetFSMSettings().targetLostDetections; 
            attackRangePow = fManager.GetFSMSettings().attackRange * 
                fManager.GetFSMSettings().attackRange;
        }
        private void Look()
        {
            if (fManager.PursueTarget == null)
            {
                notFoundCounterVal = notFoundCounter;
                fManager.SwitchState(false, fManager.patrolState);
                return;
            }

            ITeamMember[] enemiesInRange = fManager.GetEnemiesInRange();
            if (enemiesInRange.Length <= 0)
            {
                if (notFoundCounterVal <= 0)
                {
                    notFoundCounterVal = notFoundCounter;
                    fManager.PursueTarget = null;
                    fManager.SwitchState(false, fManager.patrolState);
                    return;
                }
                notFoundCounterVal--;
                return;
            }

            notFoundCounterVal = notFoundCounter;

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
                fManager.SwitchState(false, fManager.alertState);
                return;
            }
            fManager.MyNavMeshAgent.SetDestination(fManager.PursueTarget.position);
            fManager.LocationOfInterest = fManager.PursueTarget.position;
            fManager.MyNavMeshAgent.isStopped = false;


            if ((fManager.PursueTarget.position - fTransform.position).sqrMagnitude <=
                attackRangePow)
            {
                Debug.Log("Attacking the target");
                fManager.currentState = fManager.attackState;
            }

        }
        public void UpdateState()
        {
            Look();
            Pursue();
        }
    }
}
