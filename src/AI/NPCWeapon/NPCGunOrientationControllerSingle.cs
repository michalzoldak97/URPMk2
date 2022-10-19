using UnityEngine;

namespace URPMk2
{
	public class NPCGunOrientationControllerSingle : NPCGunOrientationController
	{
        [SerializeField] private Transform weapon;

        private float[] xMinMaxRotation, yMinMaxRotation;
        private void Start()
        {
            xMinMaxRotation = npcMaster.GetNPCWeaponSettings().xMinMaxRotation;
            yMinMaxRotation = npcMaster.GetNPCWeaponSettings().yMinMaxRotation;
        }
        protected override void OnTargetAttack(Transform target)
        {
            weapon.localRotation = GetDesiredRotation(
                weapon, 
                target.position, 
                xMinMaxRotation, 
                yMinMaxRotation);
        }
    }
}
