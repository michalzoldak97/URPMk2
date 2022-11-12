using System.Collections;
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
        protected Transform myTransform;
        protected RaycastHit[] hitObstacles;
        protected Collider[] colObstacles;
        private WaitForSeconds waitForRestoreDistance;
        protected virtual void Start()
        {
            myTransform = transform;
            hitObstacles = new RaycastHit[1];
            colObstacles = new Collider[1];
            waitForRestoreDistance = new WaitForSeconds(3f);
        }
        private IEnumerator RestoreDistance()
        {
            isRestoringStopDistance = true;
            yield return waitForRestoreDistance;

            if (navMeshAgent == null)
                yield break;

            navMeshAgent.stoppingDistance = stopDist;
            isRestoringStopDistance = false;
        }
        private void MoveToCleanPos(Vector3 pos)
        {
            navMeshAgent.stoppingDistance = 1.5f;
            navMeshAgent.SetDestination(pos);
            navMeshAgent.isStopped = false;
            if (!isRestoringStopDistance)
                StartCoroutine(RestoreDistance());
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
        private bool IsGrenadeFieldCleanHorizontal(float distToTarget, NPCWeaponGreanadeSettings wgs, Transform target, Transform weapon)
        {
            Vector3 checkStart = weapon.position;
            checkStart.z += wgs.checkOffset;

            int numObstacles = Physics.SphereCastNonAlloc(
                checkStart,
                wgs.horizontalObstacleCheckRadius,
                (target.position - weapon.position).normalized,
                hitObstacles,
                distToTarget,
                friendlyLayers
            );

            if (numObstacles > 0)
                return false;

            return true;
        }
        private bool IsGrenadeFieldCleanVertical(NPCWeaponGreanadeSettings wgs, Transform target)
        {
            int numObstacles = Physics.OverlapSphereNonAlloc(
                target.position,
                wgs.verticalObstacleCheckRadius,
                colObstacles,
                friendlyLayers
            );

            if (numObstacles > 0)
                return false;

            return true;
        }
        public bool IsThrowGrenadeFieldClean(float distToTarget, NPCWeaponGreanadeSettings wgs, Transform target, Transform weapon)
        {
            if (!IsGrenadeFieldCleanVertical(wgs, target) ||
                !IsGrenadeFieldCleanHorizontal(distToTarget, wgs, target, weapon))
                return false;

            return true;
        }
    }
}
