using UnityEngine;

namespace URPMk2
{
	public class NPCWeaponInputGrenade : MonoBehaviour
	{
        [SerializeField] private Transform weapon;
        [SerializeField] private Transform launcher;
        private Transform myTransform;
        private NPCMaster npcMaster;
        private NPCWeaponShootFieldValidator shootFieldValidator;
        private INPCWeaponController weaponController, launcherController;
        private void SetInit()
        {
            npcMaster = GetComponent<NPCMaster>();
            shootFieldValidator = GetComponent<NPCWeaponShootFieldValidator>();
            weaponController = weapon.GetComponent<INPCWeaponController>();
            launcherController = launcher.GetComponent<INPCWeaponController>();
        }
        private void Start()
        {
            myTransform = transform;
        }
        private void OnEnable()
        {
            SetInit();
            npcMaster.EventAttackTarget += OnAttackTarget;
        }

        private void OnDisable()
        {
            npcMaster.EventAttackTarget -= OnAttackTarget;
        }
        private float CalculateDotProd(Transform target)
        {
            return Vector3.Dot((target.position - myTransform.position).normalized, myTransform.forward);
        }

        private void AttemptToShootLauncher(Transform target)
        {
            NPCWeaponGreanadeSettings wgs = npcMaster.GetNPCWeaponSettings().greanadeSettings;
            if (!shootFieldValidator.IsThrowGrenadeFieldClean(
                wgs.horizontalObstacleCheckRadius,
                wgs.verticalObstacleCheckRadius,
                target,
                launcher))
                return;

            launcherController.LaunchHGAttack(20f, -20f, myTransform);
        }
        private void OnAttackTarget(Transform target)
        {
            if (!(CalculateDotProd(target) >
                npcMaster.GetNPCWeaponSettings().minDotProd))
                return;

            if (shootFieldValidator.IsShootFieldClean(transform))
                weaponController.LaunchAtack(myTransform);

            AttemptToShootLauncher(target);
        }
    }
}
