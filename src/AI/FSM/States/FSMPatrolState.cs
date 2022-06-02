using UnityEngine;

namespace URPMk2
{
	public class FSMPatrolState : IFSMState
	{
        // create class team identifier with team, position data
        // register every in global teams manager
        // get list of transforms in sight range and in team ordered by distance
        // patrol for items in list check if raycast
        // if not and close check if box, store box dimmensions in object passed
        private FSMStateManager fManager;
        Vector3 lookAtPoint;
        Vector3 heading;
        float dotProd;
        Collider[] colliders;

        public FSMPatrolState (FSMStateManager fManager)
        {
            this.fManager = fManager;
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
            lookAtPoint = new Vector3(target.position.x, target.position.y + fManager.GetFSMSettings().lookOffset, target.position.z);
            heading = lookAtPoint - fManager.transform.position;
            dotProd = Vector3.Dot(heading, fManager.transform.forward);
        }
        private void Look()
        {
            colliders = Physics.OverlapSphere(fManager.transform.position, fManager.GetFSMSettings().sightRange / 3, fManager.GetFSMSettings().enemyLayers);

            if (colliders.Length > 0)
            {
                CalculateVisibility(colliders[0].transform);

                if (dotProd > 0)
                {
                    SetUpAlertState(colliders[0].transform);
                    return;
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
