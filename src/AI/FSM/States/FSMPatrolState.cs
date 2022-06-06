using System;
using System.Collections.Generic;
using UnityEngine;

namespace URPMk2
{
    public class FSMPatrolState : IFSMState
	{
        private float dotProd;
        private Vector3 lookAtPoint;
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
        private void CalculateVisibility(Transform target)
        {
            lookAtPoint.x = target.position.x; lookAtPoint.y = target.position.y; lookAtPoint.z = target.position.z;
            heading = lookAtPoint - fTransform.position;
            dotProd = Vector3.Dot(heading, fTransform.forward);
        }
        private void Look()
        {
            enemyCols = Physics.OverlapSphere(fTransform.position, fManager.GetFSMSettings().highResDetectionRange, fManager.GetFSMSettings().enemyLayers);

            Array.Sort(enemyCols, fManager.priorityComparer);

            int closeLen = enemyCols.Length;
            if (closeLen > 0)
            {
                for (int i = 0; i < closeLen; i++)
                {
                    if (!Array.Exists(fManager.GetFSMSettings().tagsToAttack, el => enemyCols[i].CompareTag(el)))
                        continue;

                    CalculateVisibility(enemyCols[i].transform);

                    if (dotProd > 0)
                    {
                        SetUpAlertState(enemyCols[i].transform);
                        return;
                    }
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
