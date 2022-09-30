using UnityEngine;

namespace URPMk2
{
	public class NPCGunOrientationController : MonoBehaviour
	{
        protected NPCMaster npcMaster;
        protected virtual void SetInit()
        {
            npcMaster = GetComponent<NPCMaster>();
        }
        private void OnEnable()
        {
            SetInit();
            npcMaster.EventAttackTarget += OnTargetAttack;
        }
        private void OnDisable()
        {
            npcMaster.EventAttackTarget -= OnTargetAttack;
        }
        private float Clamp(float val, float[] minMax)
        {
            if (val > minMax[1])
                val = minMax[1];
            else if (val < minMax[0])
                val = minMax[0];

            return val;
        }
        protected Quaternion GetDesiredRotation(Transform weaponTransform, Vector3 targetPos, float[] xLimits, float[] yLimits)
        {
            Quaternion dRot = Quaternion.LookRotation(targetPos - weaponTransform.position, Vector3.up);
            weaponTransform.rotation = dRot;
            Vector3 dRotAngles = weaponTransform.rotation.eulerAngles;
            dRotAngles.x = (dRotAngles.x > 180) ? dRotAngles.x - 360 : dRotAngles.x;
            dRotAngles.x = Mathf.Clamp(dRotAngles.x, xLimits[0], xLimits[1]);

            dRot = Quaternion.Euler(dRotAngles);


            return dRot;
        }
		protected virtual void OnTargetAttack(Transform target)
        {

        }
	}
}
