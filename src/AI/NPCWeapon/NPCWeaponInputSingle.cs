using UnityEngine;

namespace URPMk2
{
	public class NPCWeaponInputSingle : NPCWeaponInput
	{
		[SerializeField] private Transform weapon;
        private INPCWeaponController weaponController;
        protected override void Start()
        {
            base.Start();
            weaponController = weapon.GetComponent<INPCWeaponController>();
        }
        protected override void OnAttackTarget(Transform target)
        {
            AttemptToShoot(target, weaponController);
        }
    }
}
