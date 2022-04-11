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
		private bool IsNotItemWeapon()
        {
			return playerInventory.selectedItem == null || 
				playerInventory.selectedItem.GetComponent<WeaponMaster>() == null;
		}
		private void CallAimOnWeapon(InputAction.CallbackContext obj)
        {
			if (IsNotItemWeapon()) return;
			playerInventory.selectedItem.GetComponent<WeaponMaster>().CallEventAimRequest();
		}
		private void CallPullTrigger(InputAction.CallbackContext obj)
        {
			if (IsNotItemWeapon()) return;
			playerInventory.selectedItem.GetComponent<WeaponMaster>().CallEventPullTrigger();
		}
		private void CallReleaseTrigger(InputAction.CallbackContext obj)
		{
			if (IsNotItemWeapon()) return;
			playerInventory.selectedItem.GetComponent<WeaponMaster>().CallEventReleaseTrigger();
		}
		private void CallReloadRequest(InputAction.CallbackContext obj)
		{
			if (IsNotItemWeapon()) return;
			playerInventory.selectedItem.GetComponent<WeaponMaster>().CallEventReloadRequest();
		}
		private void CallChangeFireMode(InputAction.CallbackContext obj)
		{
			if (IsNotItemWeapon()) return;
			playerInventory.selectedItem.GetComponent<WeaponMaster>().CallEventFireModeChangeRequest();
		}
	}
}
