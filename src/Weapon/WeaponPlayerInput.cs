using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace URPMk2
{
	public class WeaponPlayerInput : MonoBehaviour, IActionMapChangeSensitive
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
			WeaponSettingsSO weaponSettings = weaponMaster.GetWeaponSettings();
			shootRate = 60f / weaponSettings.gunSettings.shootRate;
			waitNextShootAuto = new WaitForSeconds(shootRate);
		}

        private void OnEnable()
		{
			SetInit();
			InputManager.playerInputActions.Humanoid.Shoot.started += PullTrigger;
			InputManager.playerInputActions.Humanoid.Shoot.performed += ReleaseTrigger;
			InputManager.playerInputActions.Humanoid.Shoot.Enable();
			InputManager.actionMapChange += InputMapChange;
		}
		
		private void OnDisable()
		{
			InputManager.playerInputActions.Humanoid.Shoot.started -= PullTrigger;
			InputManager.playerInputActions.Humanoid.Shoot.performed -= ReleaseTrigger;
			InputManager.playerInputActions.Humanoid.Shoot.Disable();
			InputManager.actionMapChange -= InputMapChange;
		}
		private void AttempShoot()
        {
			Debug.Log("Shoot    " + weaponMaster.isShootState);
        }
		private void SingleShoot()
        {
			AttempShoot();
		}
		private IEnumerator AutoShoot()
        {
            while (weaponMaster.isShootState)
            {
				AttempShoot();
				yield return waitNextShootAuto;
            }
        }
		private IEnumerator BurstShoot()
        {
			weaponMaster.isShootingBurst = true;
			BurstFireSettings burstFireSettings = weaponMaster.GetWeaponSettings().burstFireSettings;
			WaitForSeconds waitNextShootBurst = new WaitForSeconds(60f / burstFireSettings.burstShootRate);
			for (int i = 0; i < burstFireSettings.shootsInBurst; i++)
            {
				AttempShoot();
				yield return waitNextShootBurst;
			}
			weaponMaster.isShootingBurst = false;
		}
		private void ReleaseTrigger(InputAction.CallbackContext obj)
        {
			if (itemMaster.isSelectedOnParent)
				weaponMaster.isShootState = false;
		}
		private void PullTrigger(InputAction.CallbackContext obj)
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
				default:	break;
            }
		}
		public void InputMapChange(InputActionMap actionMapToSet)
        {
			weaponMaster.isShootState = false;
		}
	}
}
