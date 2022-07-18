using System;
using System.Collections.Generic;
using UnityEngine;

/**/

namespace URPMk2
{
    public class FSMPatrolState : IFSMState
	{
        private Vector3 heading;
        private Transform fTransform;
        private Collider[] enemyCols;
        private FSMStateManager fManager;

        public FSMPatrolState (FSMStateManager fManager)
        {
            this.fManager = fManager;
            fTransform = fManager.transform;
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
        private float CalculateVisibility(Transform target)
        {
            heading = Vector3.Normalize(target.position - fTransform.position);
            return Vector3.Dot(heading, fTransform.forward);
        }
        private void Look()
        {
            List<ITeamMember> enemiesInRange = TeamMembersManager.GetTeamMembersInRange(
                    fManager.GetFSMSettings().teamsToAttack,
                    fManager.transform.position,
                    fManager.GetFSMSettings().sightRange * fManager.GetFSMSettings().sightRange
                );

            int numEnemies = enemiesInRange.Count;
            for (int i = 0; i < numEnemies; i++)
            {
                Debug.Log("ID: " + enemiesInRange[i].TeamID + " dot prod is: " + CalculateVisibility(enemiesInRange[i].ObjTransform));
                if (VisibilityCalculator.IsVisibleSingle(fManager.VisibilityParams, enemiesInRange[i]))
                {
                    Debug.Log("\n");
                }
            }
        }
        private void Patrol()
        {

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
