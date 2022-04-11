using UnityEngine;
using UnityEngine.InputSystem;

namespace URPMk2
{
	public class WeaponPlayerInputReload : MonoBehaviour
	{
		private WeaponMaster weaponMaster;
		private ItemMaster itemMaster;
		private void SetInit()
		{
			weaponMaster = GetComponent<WeaponMaster>();
			itemMaster = GetComponent<ItemMaster>();
		}
        private void Start()
        {
			InputManager.playerInputActions.Humanoid.Reload.Enable();
		}
        private void OnEnable()
		{
			SetInit();
			InputManager.playerInputActions.Humanoid.Reload.performed += HandleReloadRequest;
		}
		
		private void OnDisable()
		{
			InputManager.playerInputActions.Humanoid.Reload.performed -= HandleReloadRequest;
		}
		private void HandleReloadRequest(InputAction.CallbackContext obj)
        {
			if (!itemMaster.isSelectedOnParent ||
				weaponMaster.isReloading ||
				weaponMaster.isAim)
				return;

			weaponMaster.CallEventReloadRequest();
        }
	}
}
