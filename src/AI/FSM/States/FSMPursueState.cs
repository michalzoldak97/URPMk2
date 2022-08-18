using UnityEngine;

namespace URPMk2
{
	public class FSMPursueState : IFSMState
	{
        private FSMStateManager fManager;
        public FSMPursueState(FSMStateManager fManager)
        {
            this.fManager = fManager;
        }
        private void Look()
        {
            if (fManager.PursueTarget == null)
            {
                ToPatrolState();
                return;
            }

            FSMTarget target = fManager.IsTargetVisible();
            if (!target.isVisible)
            {
                fManager.PursueTarget = null;
                ToPatrolState();
                return;
            }


        }
        private void Pursue()
        {

        }
        public void UpdateState()
        {
            Look();
            Pursue();
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
