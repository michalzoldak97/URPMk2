using UnityEngine;
using UnityEngine.InputSystem;

namespace URPMk2
{
	public class WeaponAimPosition : MonoBehaviour
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
			float[] aimPositionToSet = weaponMaster.GetWeaponSettings().aimPosition;
			aimPosition = Utils.GetVector3FromFloat(aimPositionToSet);
		}

        private void OnEnable()
		{
			SetInit();
			InputManager.playerInputActions.Humanoid.Aim.performed += HandleAim;
			InputManager.playerInputActions.Humanoid.Aim.Enable();
		}
		
		private void OnDisable()
		{
			InputManager.playerInputActions.Humanoid.Aim.performed -= HandleAim;
			InputManager.playerInputActions.Humanoid.Aim.Disable();
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
			if (!aimRequested)
			{
				startPosition = transform.localPosition;
				Aim(true, aimPosition);
			}
			else
				Aim(false, startPosition);
        }
	}
}
