using UnityEngine;

namespace URPMk2
{
	public class FSMStruckState : IFSMState
    {
        private readonly FSMStateManager fManager;
        public FSMStruckState(FSMStateManager fManager)
        {
            this.fManager = fManager;
        }
        public void UpdateState()
        {

        }
        public void ToPatrolState()
        {

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
