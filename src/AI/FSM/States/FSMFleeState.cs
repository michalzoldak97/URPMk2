using UnityEngine;

namespace URPMk2
{
	public class FSMFleeState : IFSMState
    {
        private readonly FSMStateManager fManager;

        public FSMFleeState(FSMStateManager fManager)
        {
            this.fManager = fManager;
        }
        private void CheckShouldFlee()
        {

        }
        private void CheckShouldFight()
        {

        }
        public void UpdateState()
        {
            CheckShouldFlee();
            CheckShouldFight();
        }
        public void ToPatrolState()
        {
            fManager.MyNavMeshAgent.isStopped = false;
            fManager.currentState = fManager.patrolState;
        }
        public void ToAlertState()
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
