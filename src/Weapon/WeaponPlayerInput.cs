using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace URPMk2
{
	public class WeaponPlayerInput : MonoBehaviour, IActionMapChangeSensitive
	{
		private bool isShootState;
		private float shootRate;
		private WaitForSeconds waitNextShoot;
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
			waitNextShoot = new WaitForSeconds(shootRate);
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
			Debug.Log("Shoot    " + isShootState);
        }

		private IEnumerator AutoShoot()
        {
            while (isShootState)
            {
				AttempShoot();
				yield return waitNextShoot;
            }
        }
		private void ReleaseTrigger(InputAction.CallbackContext obj)
        {
			if (itemMaster.isSelectedOnParent)
				isShootState = false;
		}
		private void PullTrigger(InputAction.CallbackContext obj)
        {
			if (itemMaster.isSelectedOnParent)
			{
				isShootState = true;
				StartCoroutine(AutoShoot());
			}
		}
		public void InputMapChange(InputActionMap actionMapToSet)
        {
			isShootState = false;
		}
	}
}
