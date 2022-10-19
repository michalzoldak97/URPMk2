using UnityEngine;

namespace URPMk2
{
	public class NPCWeaponHGController : MonoBehaviour, INPCWeaponController
    {
        private WeaponMaster weaponMaster;
        private void Start()
        {
            weaponMaster = GetComponent<WeaponMaster>();
        }
        public void LaunchAtack(Transform origin){}
        public void LaunchHGAttack(float force, float angle, Transform origin) 
        {
            weaponMaster.SetDmgOrigin(origin);
            weaponMaster.CallEventThrowGrenade(force, angle);
        }
    }
}
