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
		}
		
		private void OnDisable()
		{
			weaponMaster.EventShoot -= OnShoot;
			weaponMaster.EventReloadRequest -= OnReload;
			itemMaster.EventItemPickedUp -= SetAmmoMaster;
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
		private void OnReload()
        {
			int amountToRequest = weaponMaster.GetWeaponSettings().ammoCapacity - currentAmmo;
			if (amountToRequest < 1 || ammoMaster == null)
				return;

			Debug.Log("Event ammo change called from gun");
			ammoMaster.CallEventAmmoChange(currentAmmoCode, -amountToRequest, this);
			if (currentAmmo > 0)
				weaponMaster.isWeaponLoaded = true;
		}
		private void SetAmmoMaster(Transform origin)
        {
			ammoMaster = origin.parent.GetComponent<IAmmoMaster>();
        }
	}
}
