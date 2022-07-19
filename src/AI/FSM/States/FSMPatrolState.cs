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
        private float CalculateDotProd(Transform target)
        {
            heading = (target.position - fTransform.position).normalized;
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
                Debug.Log("ID: " + enemiesInRange[i].TeamID + " dot prod is: " + CalculateDotProd(enemiesInRange[i].ObjTransform));
                if (CalculateDotProd(enemiesInRange[i].ObjTransform) > 0.0f &&
                    VisibilityCalculator.IsVisibleSingle(fManager.VisibilityParams, enemiesInRange[i]))
                {
                    Debug.Log("Found  " + enemiesInRange[i].ObjTransform.name);
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
