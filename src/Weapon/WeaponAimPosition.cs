using UnityEngine;
using UnityEngine.InputSystem;

namespace URPMk2
{
	public class WeaponAimPosition : MonoBehaviour, IActionMapChangeSensitive
	{
		private bool aimRequested;
		private Vector3 startPosition, aimPosition;
		private WeaponMaster weaponMaster;
		private ItemMaster itemMaster;
		private void SetInit()
		{
			weaponMaster = GetComponent<WeaponMaster>();
			itemMaster = GetComponent<ItemMaster>();
		}
        private void Start()
        {
			float[] startPositionToSet = itemMaster.GetItemSettings().onPlayerPosition;
			startPosition = Utils.GetVector3FromFloat(startPositionToSet);
			float[] aimPositionToSet = weaponMaster.GetWeaponSettings().aimPosition;
			aimPosition = Utils.GetVector3FromFloat(aimPositionToSet);
		}

        private void OnEnable()
		{
			SetInit();
			InputManager.playerInputActions.Humanoid.Aim.performed += HandleAim;
			InputManager.playerInputActions.Humanoid.Aim.Enable();
			InputManager.actionMapChange += InputMapChange;
		}
		
		private void OnDisable()
		{
			InputManager.playerInputActions.Humanoid.Aim.performed -= HandleAim;
			InputManager.playerInputActions.Humanoid.Aim.Disable();
			InputManager.actionMapChange -= InputMapChange;
		}
		private void Aim(bool isRequested, Vector3 posToSet)
		{
			aimRequested = isRequested;
			weaponMaster.isAim = isRequested;
			if (weaponMaster.isReloading)
				return;
			transform.localPosition = posToSet;
			itemMaster.CallEventToggleItemCamera(isRequested);
		}
		private void HandleAim(InputAction.CallbackContext obj)
        {
			if (!itemMaster.isSelectedOnParent || weaponMaster.isReloading)
				return;
			if (!aimRequested)
			{
				Aim(true, aimPosition);
			}
			else
				Aim(false, startPosition);
        }
		public void InputMapChange(InputActionMap actionMapToSet)
		{
			if (!itemMaster.isSelectedOnParent || weaponMaster.isReloading)
				return;
			Aim(false, startPosition);
		}
	}
}
