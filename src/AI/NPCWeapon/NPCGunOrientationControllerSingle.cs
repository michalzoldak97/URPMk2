using UnityEngine;

namespace URPMk2
{
	public class NPCGunOrientationControllerSingle : NPCGunOrientationController
	{
        [SerializeField] private Transform weapon;
		[SerializeField] private NPCWeaponSO weaponSettings;

        protected override void OnTargetAttack(Transform target)
        {
            weapon.rotation = GetDesiredRotation(
                weapon, 
                target.position, 
                weaponSettings.xMinMaxRotation, 
                weaponSettings.yMinMaxRotation);
            
        }
    }
}
