using UnityEngine;

namespace URPMk2
{
	public class NPCWeaponInputTurrent : NPCWeaponInput
    {
        [SerializeField] private Transform weapon;
        private Transform dmgOrigin;
        private INPCWeaponController weaponController;
        protected override void Start()
        {
            base.Start();
            weaponController = weapon.GetComponent<INPCWeaponController>();
            dmgOrigin = transform;
        }
        protected override void AttemptToShoot(Transform target, INPCWeaponController weaponController)
        {
            if (CalculateDotProd(target) > .9f)
                weaponController.LaunchAtack(dmgOrigin);
        }
        protected override void OnAttackTarget(Transform target)
        {
            AttemptToShoot(target, weaponController);
        }
    }
}
