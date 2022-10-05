using UnityEngine;
using UnityEngine.AI;

namespace URPMk2
{
	public class NPCWeaponShootFieldValidator : MonoBehaviour
	{
        private bool isRestoringStopDistance;
		protected int attackRange;
        protected float stopDist, shiftRange;
		protected LayerMask friendlyLayers;
		protected NavMeshAgent navMeshAgent;
        protected virtual void Start()
        {

		}
        private async void RestoreDistance()
        {
            isRestoringStopDistance = true;
            await System.TimeSpan.FromSeconds(3f);
            navMeshAgent.stoppingDistance = stopDist;
            isRestoringStopDistance = false;
        }
        private void MoveToCleanPos(Vector3 pos)
        {
            navMeshAgent.stoppingDistance = 1.5f;
            navMeshAgent.SetDestination(pos);
            navMeshAgent.isStopped = false;
            if (!isRestoringStopDistance)
                RestoreDistance();
        }
        private void TryToChangePos(Transform pos)
        {
            bool foundCleanPos = false;
            int numAttempts = 0;
            Vector3 rndPoint = pos.position;
            rndPoint.x += Random.Range(-shiftRange, shiftRange);
            rndPoint.y += Random.Range(-shiftRange, shiftRange);

            while (!foundCleanPos &&
                numAttempts < 10)
            {
                if (Physics.Raycast(rndPoint, -Vector3.up * rndPoint.y, out RaycastHit hit, shiftRange + 1f))
                    rndPoint = hit.point;

                if (NavMesh.SamplePosition(rndPoint, out NavMeshHit navHit, shiftRange, NavMesh.AllAreas))
                {
                    MoveToCleanPos(navHit.position);
                    foundCleanPos = true;
                }
                rndPoint = Random.insideUnitSphere * 3f;
                numAttempts++;
            }
        }
        public bool IsShootFieldClean(Transform weapon)
        {
			if (Physics.Raycast(
				weapon.position,
				weapon.forward,
				out RaycastHit hit,
				attackRange,
				friendlyLayers
				))
            {
                TryToChangePos(weapon);
                return false;
            }
            return true;
        }
    }
}
