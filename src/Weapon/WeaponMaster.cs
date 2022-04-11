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
		public event WeaponInputEvenHandler EventFireModeChanged;

		public delegate void WeaponEventHandler();
		public event WeaponEventHandler EventShoot;
		public event WeaponEventHandler EventReload;
		public event WeaponEventHandler EventUpdateAmmoUI;

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
		public void CallEventFireModeChanged()
        {
			EventFireModeChanged?.Invoke();
		}
		public void CallEventEventUpdateAmmoUI()
		{
			EventUpdateAmmoUI?.Invoke();
		}
		private void Start()
        {
			fireMode = (WeaponFireMode)weaponSettings.gunSettings.defaultFireMode;
        }
    }
}
