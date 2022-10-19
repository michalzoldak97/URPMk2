using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

namespace URPMk2
{
	public class NPCWeaponShootFieldValidator : MonoBehaviour
	{
        private bool isRestoringStopDistance;
		protected int attackRange;
        protected float stopDist, shiftRange;
		protected LayerMask friendlyLayers;
		protected NavMeshAgent navMeshAgent;
        protected Transform myTransform;
        protected RaycastHit[] obstacles;
        protected virtual void Start()
        {
            myTransform = transform;
            obstacles = new RaycastHit[1];
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
				out _,
				attackRange,
				friendlyLayers
				))
            {
                TryToChangePos(weapon);
                return false;
            }
            return true;
        }
        private bool IsGrenadeFieldCleanHorizontal(float hCheckRadius, Transform target, Transform weapon)
        {
            float distToTarget = Vector3.Distance(myTransform.position, target.position);
            Vector3 checkStart = weapon.position;
            checkStart.z += 2f;
            int numObstacles = Physics.SphereCastNonAlloc(
                checkStart,
                hCheckRadius,
                (target.position - weapon.position).normalized,
                obstacles,
                distToTarget,
                friendlyLayers);

            Debug.Log("Found: " + numObstacles + " obstacles");

            if (numObstacles > 0)
                return false;

            return true;
        }
        public bool IsThrowGrenadeFieldClean(float hCheckRadius, float vCheckRadius, Transform target, Transform weapon)
        {
            if (!IsGrenadeFieldCleanHorizontal(hCheckRadius, target, weapon))
                return false;

            return true;
        }
    }
}
