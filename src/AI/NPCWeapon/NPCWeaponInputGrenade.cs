using UnityEngine;

namespace URPMk2
{
	public class NPCWeaponInputGrenade : MonoBehaviour
	{
        [SerializeField] private Transform weapon;
        [SerializeField] private Transform launcher;
        private Transform myTransform;
        private NPCMaster npcMaster;
        private WeaponMaster launcherMaster;
        private NPCWeaponShootFieldValidator shootFieldValidator;
        private INPCWeaponController weaponController, launcherController;
        private void SetInit()
        {
            npcMaster = GetComponent<NPCMaster>();
            shootFieldValidator = GetComponent<NPCWeaponShootFieldValidator>();
            weaponController = weapon.GetComponent<INPCWeaponController>();
            launcherController = launcher.GetComponent<INPCWeaponController>();
            launcherMaster = launcher.GetComponent<WeaponMaster>();
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

        private void AttemptToShootWeapon(Transform target)
        {
            if (!(CalculateDotProd(target) >
                npcMaster.GetNPCWeaponSettings().minDotProd))
                return;

            if (shootFieldValidator.IsShootFieldClean(weapon))
                weaponController.LaunchAtack(myTransform);
        }
        private void AttemptToShootLauncher(Transform target)
        {
            if (!launcherMaster.isWeaponLoaded)
                return;

            NPCWeaponGreanadeSettings wgs = npcMaster.GetNPCWeaponSettings().greanadeSettings;

            if (wgs.isRandomAttack &&
                Random.Range(0, 11) > wgs.randAttackChance)
                return;

            float distToTarget = Vector3.Distance(myTransform.position, target.position);

            if (distToTarget > wgs.grenadeThrowRange ||
                !shootFieldValidator.IsThrowGrenadeFieldClean(
                distToTarget,
                wgs,
                target,
                launcher
                ))
                return;

            float throwForce = (wgs.forceEquation[0] * distToTarget) + wgs.forceEquation[1];
            float throwAngle = (wgs.angleEquation[0] * distToTarget) + wgs.angleEquation[1];

            launcherController.LaunchHGAttack(throwForce, throwAngle, myTransform);
        }
        private void OnAttackTarget(Transform target)
        {
            AttemptToShootWeapon(target);
            AttemptToShootLauncher(target);
        }
    }
}
