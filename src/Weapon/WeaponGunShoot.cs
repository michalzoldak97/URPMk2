using UnityEngine;

namespace URPMk2
{
	public class WeaponGunShoot : MonoBehaviour
	{
		private WeaponMaster weaponMaster;
		private WeaponAmmo weaponAmmo;
		private void SetInit()
		{
			weaponMaster = GetComponent<WeaponMaster>();
			weaponAmmo = GetComponent<WeaponAmmo>();
		}
		
		private void OnEnable()
		{
			SetInit();
			weaponMaster.EventShootRequest += OnShoot;
		}
		
		private void OnDisable()
		{
			weaponMaster.EventShootRequest -= OnShoot;
		}
		private void OnShoot()
        {
			if (weaponAmmo.currentAmmo > 0)
			{
				weaponMaster.CallEventShoot();
				Debug.Log("Shoot    " + weaponMaster.isShootState);
			}
        }
	}
}
