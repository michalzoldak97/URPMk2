using System;
using System.Collections.Generic;
using UnityEngine;

/**/

namespace URPMk2
{
    public class FSMPatrolState : IFSMState
	{
        private int sightRangePow;
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
                if (CalculateDotProd(enemiesInRange[i].ObjTransform) < 0.0f)
                    continue;

                if (VisibilityCalculator.IsVisibleSingle(fManager.VisibilityParams, heading, enemiesInRange[i].ObjTransform)
                    || (heading.sqrMagnitude < fManager.VisibilityParams.highResSearchSqrRange &&
                    VisibilityCalculator.IsVisibleCorners(fManager.VisibilityParams, enemiesInRange[i].ObjTransform, enemiesInRange[i].BoundsExtens)))
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
