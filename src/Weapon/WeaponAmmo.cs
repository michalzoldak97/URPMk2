using UnityEngine;

namespace URPMk2
{
	public class WeaponAmmo : MonoBehaviour
	{
		public int currentAmmo { get; private set; }
		private WeaponMaster weaponMaster;
		private void SetInit()
		{
			weaponMaster = GetComponent<WeaponMaster>();
		}

        private void Start()
        {
			currentAmmo = weaponMaster.GetWeaponSettings().ammoCapacity;
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
	}
}
