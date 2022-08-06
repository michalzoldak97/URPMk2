using UnityEngine;

namespace URPMk2
{
	public class FSMAlertState : IFSMState
	{
        private int enemyDetections;
        private Transform fTransform;
        private FSMStateManager fManager;

        public FSMAlertState(FSMStateManager fManager)
        {
            this.fManager = fManager;
            fTransform = fManager.transform;
        }
        public void UpdateState()
        {

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
