using UnityEngine;
using UnityEngine.AI;

namespace URPMk2
{
	public class FSMFleeState : IFSMState
    {
        private readonly Transform fTransform;
        private readonly FSMStateManager fManager;

        public FSMFleeState(FSMStateManager fManager)
        {
            this.fManager = fManager;
            fTransform = fManager.transform;
        }
        private void CheckShouldFlee()
        {
            FSMTarget possibleEnemy = fManager.IsTargetVisible();
            if (!possibleEnemy.isVisible)
            {
                fManager.SwitchState(false, fManager.patrolState);
                return;
            }

            Vector3 fleeDirection = fTransform.position + 
                (fTransform.position - possibleEnemy.targetTransform.position);

            if (NavMesh.SamplePosition(fleeDirection, out NavMeshHit navHit, 3.0f, NavMesh.AllAreas))
            {
                fManager.MyNavMeshAgent.SetDestination(navHit.position);
                fManager.MyNavMeshAgent.isStopped = false;
            }
            else
                fManager.MyNavMeshAgent.isStopped = true;

        }
        private void CheckShouldFight()
        {
            if (fManager.PursueTarget == null)
                return;

            if ((fManager.PursueTarget.position - fTransform.position).sqrMagnitude <=
                    fManager.GetFSMSettings().fleeAttackRangePow)
                fManager.SwitchState(false, fManager.attackState);
        }
        public void UpdateState()
        {
            CheckShouldFlee();
            CheckShouldFight();
        }
    }
}
