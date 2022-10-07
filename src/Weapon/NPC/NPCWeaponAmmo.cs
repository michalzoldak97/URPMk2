using UnityEngine;

namespace URPMk2
{
	public class NPCWeaponAmmo : WeaponAmmo
	{
		protected override void SetInit()
		{
			weaponMaster = GetComponent<WeaponMaster>();
		}
		protected override void Start()
		{
			base.Start();
			ammoMaster = transform.root.GetComponent<IAmmoMaster>();
		}
		protected override void OnEnable()
		{
			SetInit();
			weaponMaster.EventShoot += OnShoot;
			weaponMaster.EventReloadRequest += OnReload;
		}

		protected override void OnDisable()
		{
			weaponMaster.EventShoot -= OnShoot;
			weaponMaster.EventReloadRequest -= OnReload;
			BreakReloadProcess(transform);
		}
        protected override void OnReload()
        {
			if (weaponMaster.isReloading ||
				weaponMaster.isAim ||
				weaponMaster.isShootState ||
				weaponMaster.isShootingBurst)
				return;

			int amountToRequest = weaponMaster.GetWeaponSettings().ammoCapacity - currentAmmo;
			if (amountToRequest < 1 || ammoMaster == null)
				return;

			StartCoroutine(ReloadAmmo(amountToRequest));
		}
    }
}
