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
		}
		
		private void OnDisable()
		{
			weaponMaster.EventShoot -= OnShoot;
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
		private void SetAmmoMaster(Transform origin)
        {
			ammoMaster = origin.parent.GetComponent<IAmmoMaster>();
        }
	}
}
