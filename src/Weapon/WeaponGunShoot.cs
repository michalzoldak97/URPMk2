using UnityEngine;

namespace URPMk2
{
	public class WeaponGunShoot : MonoBehaviour
	{
		private WeaponMaster weaponMaster;
		private void SetInit()
		{
			weaponMaster = GetComponent<WeaponMaster>();
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
			weaponMaster.CallEventShoot();
        }
	}
}
