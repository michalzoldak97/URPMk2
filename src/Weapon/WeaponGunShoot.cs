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
			weaponMaster.EventShoot += OnShoot;
		}
		
		private void OnDisable()
		{
			weaponMaster.EventShoot -= OnShoot;
		}
		private void OnShoot()
        {
			Debug.Log("Shoot    " + weaponMaster.isShootState);
        }
	}
}
