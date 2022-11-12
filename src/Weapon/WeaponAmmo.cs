using System.Collections;
using UnityEngine;

namespace URPMk2
{
	public class WeaponAmmo : MonoBehaviour
	{
		public int currentAmmo { get; protected set; }
		public string currentAmmoCode { get; private set; }
		public IAmmoMaster ammoMaster { get; protected set; }
		protected WeaponMaster weaponMaster;
		private ItemMaster itemMaster;
		protected virtual void SetInit()
		{
			weaponMaster = GetComponent<WeaponMaster>();
			itemMaster = GetComponent<ItemMaster>();
		}

        protected virtual void Start()
        {
			WeaponSettingsSO weaponSettings = weaponMaster.GetWeaponSettings();
			currentAmmo = weaponSettings.ammoCapacity;
			currentAmmoCode = weaponSettings.defaultAmmoCode;
		}

		protected virtual void OnEnable()
		{
			SetInit();
			weaponMaster.EventShoot += OnShoot;
			weaponMaster.EventReloadRequest += OnReload;
			itemMaster.EventItemPickedUp += SetAmmoMaster;
			itemMaster.EventItemThrow += BreakReloadProcess;
		}

		protected virtual void OnDisable()
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

			weaponMaster.CallEventEventUpdateAmmoUI();
        }
		protected virtual void OnShoot()
        {
			if (currentAmmo > 1)
            {
				currentAmmo--;
            }
            else
            {
				currentAmmo = 0;
                weaponMaster.SetIsWeaponLoaded(false);
            }
			weaponMaster.CallEventEventUpdateAmmoUI();
		}
		protected IEnumerator ReloadAmmo(int amountToRequest)
        {
			weaponMaster.SetIsReloading(true);
			weaponMaster.CallEventStartReload();
			yield return new WaitForSeconds(weaponMaster.GetWeaponSettings().reloadTime);
			
			ammoMaster.CallEventAmmoChange(currentAmmoCode, -amountToRequest, this);
			if (currentAmmo > 0)
                weaponMaster.SetIsWeaponLoaded(true);

            weaponMaster.CallEventReload();
            weaponMaster.SetIsReloading(false);

        }
		protected virtual void OnReload()
        {
			if (!itemMaster.isSelectedOnParent ||
				weaponMaster.isReloading ||
				weaponMaster.isAim ||
				weaponMaster.isShootState ||
				weaponMaster.isShootingBurst)
				return;

			int amountToRequest = weaponMaster.GetWeaponSettings().ammoCapacity - currentAmmo;
			if (amountToRequest < 1 || ammoMaster == null)
				return;

			StartCoroutine(ReloadAmmo(amountToRequest));
		}
		private void SetAmmoMaster(Transform origin)
        {
			ammoMaster = origin.parent.GetComponent<IAmmoMaster>();
        }
		protected void BreakReloadProcess(Transform dummy)
        {
			if (!weaponMaster.isReloading)
				return;

			StopAllCoroutines();
            weaponMaster.SetIsWeaponLoaded(currentAmmo > 0);
            weaponMaster.SetIsReloading(false);
        }
	}
}
