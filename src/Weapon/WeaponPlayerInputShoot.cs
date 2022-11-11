using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace URPMk2
{
	public class WeaponPlayerInputShoot : MonoBehaviour, IActionMapChangeSensitive
	{
		private float shootRate;
		private WaitForSeconds waitNextShootAuto;
		private WeaponMaster weaponMaster;
		private ItemMaster itemMaster;
		private void SetInit()
		{
			weaponMaster = GetComponent<WeaponMaster>();
			itemMaster = GetComponent<ItemMaster>();
		}
		private void Start()
		{
			shootRate = 60f / weaponMaster.GetWeaponSettings().gunSettings.shootRate;
			waitNextShootAuto = new WaitForSeconds(shootRate);
		}

		private void OnEnable()
		{
			SetInit();
			weaponMaster.EventPullTrigger += PullTrigger;
			weaponMaster.EventReleaseTrigger += ReleaseTrigger;
			InputManager.actionMapChange += InputMapChange;
			itemMaster.EventItemThrow += CancelShooting;
		}

		private void OnDisable()
		{
			weaponMaster.EventPullTrigger -= PullTrigger;
			weaponMaster.EventReleaseTrigger -= ReleaseTrigger;
			InputManager.actionMapChange -= InputMapChange;
			itemMaster.EventItemThrow -= CancelShooting;
			CancelShooting(transform);
		}
		private void AttempShoot()
		{
			weaponMaster.CallEventShoot();
		}
		private async void ResetTriggerLock()
		{
            weaponMaster.isTriggerLocked = true;
			await System.TimeSpan.FromSeconds(shootRate);
			weaponMaster.isTriggerLocked = false;
        }
		private void SingleShoot()
		{
			if (weaponMaster.isTriggerLocked)
				return;

			AttempShoot();
			ResetTriggerLock();
        }
		private IEnumerator AutoShoot()
		{
			while (weaponMaster.isShootState &&
				weaponMaster.isWeaponLoaded)
			{
				AttempShoot();
                yield return waitNextShootAuto;
			}
		}
		private IEnumerator BurstShoot()
		{
			weaponMaster.SetShootingBurst(true);
			BurstFireSettings burstFireSettings = weaponMaster.GetWeaponSettings().burstFireSettings;
			WaitForSeconds waitNextShootBurst = new WaitForSeconds(60f / burstFireSettings.burstShootRate);
			for (int i = 0; i < burstFireSettings.shootsInBurst; i++)
			{
				if (weaponMaster.isWeaponLoaded)
					AttempShoot();
				else
					break;
				yield return waitNextShootBurst;
			}
            weaponMaster.SetShootingBurst(false);
        }
		private void ReleaseTrigger()
		{
			if (itemMaster.isSelectedOnParent)
				weaponMaster.isShootState = false;
		}
		private void PullTrigger()
		{
			if (!itemMaster.isSelectedOnParent ||
				weaponMaster.isReloading ||
				!weaponMaster.isWeaponLoaded ||
				weaponMaster.isShootingBurst)
				return;

			weaponMaster.isShootState = true;

			switch (weaponMaster.fireMode)
			{
				case WeaponFireMode.Single:
					{
						SingleShoot();
						break;
					}
				case WeaponFireMode.Auto:
					{
						StartCoroutine(AutoShoot());
						break;
					}
				case WeaponFireMode.Burst:
					{
						StartCoroutine(BurstShoot());
						break;
					}
				default: break;
			}
		}
		public void InputMapChange(InputActionMap actionMapToSet)
		{
			weaponMaster.isShootState = false;
		}
		private void CancelShooting(Transform dummy)
        {
			StopAllCoroutines();
			weaponMaster.isShootState = false;
            weaponMaster.SetShootingBurst(false);
        }
	}
}
