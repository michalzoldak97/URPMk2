using UnityEngine;
using UnityEngine.InputSystem;

namespace URPMk2
{
	public class WeaponChangeFireMode : MonoBehaviour
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
			InputManager.playerInputActions.Humanoid.ChangeFireMode.Enable();
		}
        private void OnEnable()
		{
			SetInit();
			InputManager.playerInputActions.Humanoid.ChangeFireMode.performed += CallChangeFireMode;
		}
		
		private void OnDisable()
		{
			InputManager.playerInputActions.Humanoid.ChangeFireMode.performed -= CallChangeFireMode;
		}
		private void ChangeFireMode()
        {
			GunSettings gunSettings = weaponMaster.GetWeaponSettings().gunSettings;
			int numOfAvaliableModes = gunSettings.avaliableFireModes.Length;
			if (numOfAvaliableModes < 2)
				return;

			int currentModeIdx = -1;
			for (int i = 0; i < numOfAvaliableModes; i++)
            {
				if (i == (int)weaponMaster.fireMode)
                {
					currentModeIdx = i;
					break;
				}
            }
			if (currentModeIdx == -1)
				return;

			int nextFireMode = currentModeIdx + 1;
			if (nextFireMode < numOfAvaliableModes)
				weaponMaster.fireMode = (WeaponFireMode)gunSettings.avaliableFireModes[nextFireMode];
			else
				weaponMaster.fireMode = (WeaponFireMode)gunSettings.avaliableFireModes[0];

			weaponMaster.CallEventFireModeChanged();
		}
		private void CallChangeFireMode(InputAction.CallbackContext obj)
        {
			if (!itemMaster.isSelectedOnParent || 
				weaponMaster.isReloading ||
				weaponMaster.isShootingBurst ||
				weaponMaster.isShootState)
				return;
			ChangeFireMode();
		}
	}
}
