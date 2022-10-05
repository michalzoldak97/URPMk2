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
        protected Quaternion GetDesiredRotation(Transform weaponTransform, Vector3 targetPos, float[] xLimits, float[] yLimits)
        {
            weaponTransform.rotation = Quaternion.LookRotation(targetPos - weaponTransform.position, Vector3.up);
            Vector3 dRotAngles = weaponTransform.localEulerAngles;
            if (dRotAngles.x > 180)
                dRotAngles.x -= 360;
            if (dRotAngles.y > 180)
                dRotAngles.y -= 360;

            dRotAngles.x = Mathf.Clamp(dRotAngles.x, xLimits[0], xLimits[1]);
            dRotAngles.y = Mathf.Clamp(dRotAngles.y, yLimits[0], yLimits[1]);

            return Quaternion.Euler(dRotAngles);
        }
		protected virtual void OnTargetAttack(Transform target)
        {

        }
	}
}
