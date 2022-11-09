using UnityEngine;
using UnityEngine.InputSystem;

namespace URPMk2
{
	public class PlayerWeaponInteraction : MonoBehaviour
	{
		private PlayerInventoryManager playerInventory;
		private void SetInit()
		{
			playerInventory = GetComponent<PlayerInventoryManager>();
		}
		
		private void OnEnable()
		{
			SetInit();
			InputManager.playerInputActions.Humanoid.Aim.performed += CallAimOnWeapon;
			InputManager.playerInputActions.Humanoid.Aim.Enable();
			InputManager.playerInputActions.Humanoid.Shoot.started += CallPullTrigger;
			InputManager.playerInputActions.Humanoid.Shoot.performed += CallReleaseTrigger;
			InputManager.playerInputActions.Humanoid.Shoot.Enable();
			InputManager.playerInputActions.Humanoid.Reload.performed += CallReloadRequest;
			InputManager.playerInputActions.Humanoid.Reload.Enable();
			InputManager.playerInputActions.Humanoid.ChangeFireMode.performed += CallChangeFireMode;
			InputManager.playerInputActions.Humanoid.ChangeFireMode.Enable();
		}
		
		private void OnDisable()
		{
			InputManager.playerInputActions.Humanoid.Aim.performed -= CallAimOnWeapon;
			InputManager.playerInputActions.Humanoid.Aim.Disable();
			InputManager.playerInputActions.Humanoid.Shoot.started -= CallPullTrigger;
			InputManager.playerInputActions.Humanoid.Shoot.performed -= CallReleaseTrigger;
			InputManager.playerInputActions.Humanoid.Shoot.Disable();
			InputManager.playerInputActions.Humanoid.Reload.performed -= CallReloadRequest;
			InputManager.playerInputActions.Humanoid.Reload.Disable();
			InputManager.playerInputActions.Humanoid.ChangeFireMode.performed -= CallChangeFireMode;
			InputManager.playerInputActions.Humanoid.ChangeFireMode.Disable();
		}
		private WeaponMaster ItemWeaponMaster()
        {
			if (playerInventory.selectedItem == null)
				return null;

			return playerInventory.selectedItem.GetComponent<WeaponMaster>();
		}
		private void CallAimOnWeapon(InputAction.CallbackContext obj)
        {
			WeaponMaster itemWeaponMaster = ItemWeaponMaster();
			if (itemWeaponMaster == null) return;

			itemWeaponMaster.SetDmgOrigin(transform);
			itemWeaponMaster.CallEventAimRequest();
		}
		private void CallPullTrigger(InputAction.CallbackContext obj)
        {
			WeaponMaster itemWeaponMaster = ItemWeaponMaster();
			if (itemWeaponMaster == null) return;

			itemWeaponMaster.SetDmgOrigin(transform);
			itemWeaponMaster.CallEventPullTrigger();
		}
		private void CallReleaseTrigger(InputAction.CallbackContext obj)
		{
			WeaponMaster itemWeaponMaster = ItemWeaponMaster();
			if (itemWeaponMaster == null) return;

			itemWeaponMaster.SetDmgOrigin(transform);
			itemWeaponMaster.CallEventReleaseTrigger();
		}
		private void CallReloadRequest(InputAction.CallbackContext obj)
		{
			WeaponMaster itemWeaponMaster = ItemWeaponMaster();
			if (itemWeaponMaster == null) return;

			itemWeaponMaster.SetDmgOrigin(transform);
			itemWeaponMaster.CallEventReloadRequest();
		}
		private void CallChangeFireMode(InputAction.CallbackContext obj)
		{
			WeaponMaster itemWeaponMaster = ItemWeaponMaster();
			if (itemWeaponMaster == null) return;

			itemWeaponMaster.SetDmgOrigin(transform);
			itemWeaponMaster.CallEventFireModeChangeRequest();
		}
	}
}
