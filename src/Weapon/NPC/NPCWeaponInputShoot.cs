using System.Collections;
using UnityEngine;

namespace URPMk2
{
	public class NPCWeaponInputShoot : MonoBehaviour
	{
		private float shootsInBurst;
		private WaitForSeconds waitForNextBurstShoot, waitForNextShoot;
		private WeaponMaster weaponMaster;
		private void SetInit()
		{
			weaponMaster = GetComponent<WeaponMaster>();
		}
        private void Start()
        {
            waitForNextShoot = new WaitForSeconds(60f / weaponMaster.GetWeaponSettings().gunSettings.shootRate);
			BurstFireSettings burstFireSettings = weaponMaster.GetWeaponSettings().burstFireSettings;
            waitForNextBurstShoot = new WaitForSeconds(60f / burstFireSettings.burstShootRate);
			shootsInBurst = burstFireSettings.shootsInBurst;
		}
        private void OnEnable()
		{
			SetInit();
			weaponMaster.EventPullTrigger += PullTrigger;
			weaponMaster.EventReleaseTrigger += ReleaseTrigger;
		}
		
		private void OnDisable()
		{
			weaponMaster.EventPullTrigger -= PullTrigger;
			weaponMaster.EventReleaseTrigger -= ReleaseTrigger;
		}
		private void AttempShoot()
		{
			weaponMaster.CallEventShoot();
		}
        private IEnumerator ResetTriggerLock()
        {
            weaponMaster.isTriggerLocked = true;
			yield return waitForNextShoot;
            weaponMaster.isTriggerLocked = false;
        }
        private void SingleShoot()
		{
            if (weaponMaster.isTriggerLocked)
                return;

            AttempShoot();
            StartCoroutine(ResetTriggerLock());
        }
		private IEnumerator AutoShoot()
		{
			while (weaponMaster.isShootState &&
				weaponMaster.isWeaponLoaded)
			{
				AttempShoot();
				yield return waitForNextShoot;
			}
		}
		private IEnumerator BurstShoot()
		{
            weaponMaster.SetShootingBurst(true);

			for (int i = 0; i < shootsInBurst; i++)
			{
				if (weaponMaster.isWeaponLoaded)
					AttempShoot();
				else
					break;
				yield return waitForNextBurstShoot;
            }
            weaponMaster.SetShootingBurst(false);
        }
		private void PullTrigger()
		{
			if (weaponMaster.isReloading ||
				!weaponMaster.isWeaponLoaded ||
				weaponMaster.isShootingBurst)
				return;

			weaponMaster.isShootState = true;

			if (weaponMaster.fireMode == WeaponFireMode.Burst)
				StartCoroutine(BurstShoot());
			else if (weaponMaster.fireMode == WeaponFireMode.Single)
				SingleShoot();
			else if (weaponMaster.fireMode == WeaponFireMode.Auto)
				StartCoroutine(AutoShoot());
		}
		private void ReleaseTrigger()
		{
			weaponMaster.isShootState = false;
		}
	}
}
