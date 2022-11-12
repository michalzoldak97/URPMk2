using System.Collections;
using UnityEngine;

namespace URPMk2
{
	public class NPCRotationController : MonoBehaviour
	{
		protected float rotationAngularSpeed, rotationsPerCycle, nextRot, rotShift;
        private bool isRotatnig;
        private Transform myTransform;
        private Quaternion myRotation;
        private WaitForSeconds waitNextRot;
		protected virtual void SetParams() { }

        private void Start()
        {
            SetParams();
            myTransform = transform;
            rotShift = (1 - rotationAngularSpeed) / rotationsPerCycle;
            waitNextRot = new WaitForSeconds(nextRot);
        }

        private IEnumerator RotateTowardsTransform(Transform target)
        {
           
            isRotatnig = true;

            float t = rotationAngularSpeed;

            for (int i = 0; i < rotationsPerCycle; i++)
            {
                if (target == null)
                    break;
                myRotation =
                    Quaternion.LookRotation(
                        Vector3.RotateTowards(myTransform.forward, 
                        (target.position - myTransform.position),
                        rotationAngularSpeed, 
                        0.1f));
                myRotation.x = 0f;
                myRotation.z = 0f;
                myTransform.rotation = Quaternion.Slerp(myTransform.rotation, myRotation, t);
                t += rotShift;
                yield return waitNextRot;
            }

            isRotatnig = false;
        }
        public void StartRotateTowardsTransform(Transform target)
        {
            if (isRotatnig)
                return;
            StartCoroutine(RotateTowardsTransform(target));
        }
    }
}
