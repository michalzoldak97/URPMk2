using UnityEngine;

namespace URPMk2
{
	public class NPCWeaponInputShoot : MonoBehaviour
	{
		private float shootRate;
		private WeaponMaster weaponMaster;
		private void SetInit()
		{
			weaponMaster = GetComponent<WeaponMaster>();
		}
        private void Start()
        {
			shootRate = 60f / weaponMaster.GetWeaponSettings().gunSettings.shootRate;
		}
        private void OnEnable()
		{
			SetInit();
			weaponMaster.EventPullTrigger += PullTrigger;
		}
		
		private void OnDisable()
		{
			weaponMaster.EventPullTrigger -= PullTrigger;
		}
		private void AttempShoot()
		{
			weaponMaster.CallEventShoot();
		}
		private void SingleShoot()
		{
			AttempShoot();
		}
		private async void AutoShoot()
		{
			while (weaponMaster.isShootState &&
				weaponMaster.isWeaponLoaded)
			{
				AttempShoot();
				await System.TimeSpan.FromSeconds(shootRate);
			}
		}
		private async void BurstShoot()
		{
			weaponMaster.isShootingBurst = true;
			BurstFireSettings burstFireSettings = weaponMaster.GetWeaponSettings().burstFireSettings;
			float burstShootRate = 60f / burstFireSettings.burstShootRate;
			for (int i = 0; i < burstFireSettings.shootsInBurst; i++)
			{
				if (weaponMaster.isWeaponLoaded)
					AttempShoot();
				else
					break;
				await System.TimeSpan.FromSeconds(burstShootRate);
			}
			weaponMaster.isShootingBurst = false;
		}
		private void PullTrigger()
		{
			if (weaponMaster.isReloading ||
				!weaponMaster.isWeaponLoaded ||
				weaponMaster.isShootingBurst)
				return;

			weaponMaster.isShootState = true;

			if (weaponMaster.fireMode == WeaponFireMode.Burst)
				BurstShoot();
			else if (weaponMaster.fireMode == WeaponFireMode.Single)
				SingleShoot();
			else if (weaponMaster.fireMode == WeaponFireMode.Auto)
				AutoShoot();

		}
	}
}
