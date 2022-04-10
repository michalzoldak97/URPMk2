using System.Collections;
using UnityEngine;

namespace URPMk2
{
	public class WeaponAmmo : MonoBehaviour
	{
		public int currentAmmo { get; private set; }
		private string currentAmmoCode;
		private WeaponMaster weaponMaster;
		private ItemMaster itemMaster;
		private IAmmoMaster ammoMaster;
		private void SetInit()
		{
			weaponMaster = GetComponent<WeaponMaster>();
			itemMaster = GetComponent<ItemMaster>();
		}

        private void Start()
        {
			WeaponSettingsSO weaponSettings = weaponMaster.GetWeaponSettings();
			currentAmmo = weaponSettings.ammoCapacity;
			currentAmmoCode = weaponSettings.defaultAmmoCode;
		}

        private void OnEnable()
		{
			SetInit();
			weaponMaster.EventShoot += OnShoot;
			weaponMaster.EventReloadRequest += OnReload;
			itemMaster.EventItemPickedUp += SetAmmoMaster;
			itemMaster.EventItemThrow += BreakReloadProcess;
		}
		
		private void OnDisable()
		{
			weaponMaster.EventShoot -= OnShoot;
			weaponMaster.EventReloadRequest -= OnReload;
			itemMaster.EventItemPickedUp -= SetAmmoMaster;
			itemMaster.EventItemThrow -= BreakReloadProcess;
			BreakReloadProcess(transform);
		}
		public void ChangeAmmo(int amount)
        {
			if (amount > 0)
				currentAmmo += amount;
			else
				currentAmmo = currentAmmo + amount >= 0 ? currentAmmo + amount : 0;
        }
		private void OnShoot()
        {
			if (currentAmmo > 1)
            {
				currentAmmo--;
            }
            else
            {
				currentAmmo = 0;
				weaponMaster.isWeaponLoaded = false;
            }
        }
		private IEnumerator ReloadAmmo(int amountToRequest)
        {
			weaponMaster.isReloading = true;
			yield return new WaitForSeconds(weaponMaster.GetWeaponSettings().reloadTime);
			
			ammoMaster.CallEventAmmoChange(currentAmmoCode, -amountToRequest, this);
			if (currentAmmo > 0)
				weaponMaster.isWeaponLoaded = true;

			weaponMaster.CallEventReload();
			weaponMaster.isReloading = false;
		}
		private void OnReload()
        {
			int amountToRequest = weaponMaster.GetWeaponSettings().ammoCapacity - currentAmmo;
			if (amountToRequest < 1 || ammoMaster == null)
				return;

			StartCoroutine(ReloadAmmo(amountToRequest));
		}
		private void SetAmmoMaster(Transform origin)
        {
			ammoMaster = origin.parent.GetComponent<IAmmoMaster>();
        }
		private void BreakReloadProcess(Transform dummy)
        {
			if (!weaponMaster.isReloading)
				return;

			StopAllCoroutines();
			weaponMaster.isWeaponLoaded = currentAmmo > 0;
			weaponMaster.isReloading = false;
        }
	}
}
