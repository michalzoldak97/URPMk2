using UnityEngine;

namespace URPMk2
{
	public class WeaponMaster : MonoBehaviour
	{
		[SerializeField] private WeaponSettingsSO weaponSettings;
		public WeaponSettingsSO GetWeaponSettings() { return weaponSettings; }

		public bool isAim;
		public bool isReloading;
		public bool isShootState, isShootingBurst;
		public bool isWeaponLoaded = true;
		public WeaponFireMode fireMode;
		public delegate void WeaponInputEvenHandler();
		public event WeaponInputEvenHandler EventAimRequest;
		public event WeaponInputEvenHandler EventUnAim;
		public event WeaponInputEvenHandler EventShootRequest;
		public event WeaponInputEvenHandler EventReloadRequest;

		public delegate void WeaponEventhandler();
		public event WeaponEventhandler EventShoot;
		public event WeaponEventhandler EventReload;

		public void CallEventAimRequest()
        {
			EventAimRequest?.Invoke();
		}
		public void CallEventUnAim()
        {
			EventUnAim?.Invoke();
		}
		public void CallEventShootRequest()
        {
			EventShootRequest?.Invoke();
		}
		public void CallEventReloadRequest()
		{
			EventReloadRequest?.Invoke();
		}
		public void CallEventShoot()
		{
			EventShoot?.Invoke();
		}
		public void CallEventReload()
		{
			EventReload?.Invoke();
		}
		private void Start()
        {
			fireMode = (WeaponFireMode)weaponSettings.gunSettings.defaultFireMode;
        }
    }
}
