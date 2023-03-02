using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace URPMk2
{	public class WeaponGunUIManager : MonoBehaviour
	{
		[SerializeField] private TMP_Text currentAmmo, allAmmo, currentFireMode, currentAmmoCode;
		[SerializeField] private GameObject gunUICanvas;
		private Dictionary<WeaponFireMode, string> fireModes = new Dictionary<WeaponFireMode, string>()
		{
			{ WeaponFireMode.Single, "(S)" },
			{ WeaponFireMode.Auto, "(A)" },
			{ WeaponFireMode.Burst, "(B)" }
		};
		private WeaponMaster weaponMaster;
		private ItemMaster itemMaster;
		private WeaponAmmo weaponAmmo;
		private void SetInit()
		{
			weaponMaster = GetComponent<WeaponMaster>();
			itemMaster = GetComponent<ItemMaster>();
			weaponAmmo = GetComponent<WeaponAmmo>();
		}
		
		private void OnEnable()
		{
			SetInit();
			weaponMaster.EventUpdateAmmoUI += UpdateGunUI;
			weaponMaster.EventReload += UpdateGunUI;
			weaponMaster.EventFireModeChanged += UpdateGunUI;
			itemMaster.EventItemPickedUp += SetAmmoOnPickup;
			itemMaster.EventActivateOnParent += UpdateGunUI;
			itemMaster.EventActivateOnParent += ToggleUICanvas;
			itemMaster.EventDisableOnParent += ToggleUICanvas;
		}
		
		private void OnDisable()
		{
			weaponMaster.EventUpdateAmmoUI -= UpdateGunUI;
			weaponMaster.EventReload -= UpdateGunUI;
			weaponMaster.EventFireModeChanged -= UpdateGunUI;
			itemMaster.EventItemPickedUp -= SetAmmoOnPickup;
			itemMaster.EventActivateOnParent -= UpdateGunUI;
			itemMaster.EventActivateOnParent -= ToggleUICanvas;
			itemMaster.EventDisableOnParent -= ToggleUICanvas;
		}
		private void UpdateGunUI()
        {
			currentAmmo.text = weaponAmmo.currentAmmo.ToString();
			currentFireMode.text = fireModes[weaponMaster.fireMode];
			currentAmmoCode.text = weaponAmmo.currentAmmoCode;
			if (weaponAmmo.ammoMaster != null)
				allAmmo.text = weaponAmmo.ammoMaster.GetAmmoStore()[weaponAmmo.currentAmmoCode].ToString();
        }
		private void SetAmmoOnPickup(Transform origin)
        {
			allAmmo.text = origin.parent.GetComponent<IAmmoMaster>().GetAmmoStore()
				[weaponAmmo.currentAmmoCode].ToString();
			UpdateGunUI();
		}
		private void ToggleUICanvas()
        {
			if (!gunUICanvas.activeSelf)
				gunUICanvas.transform.SetParent(null);
			else
				gunUICanvas.transform.SetParent(transform);
			gunUICanvas.SetActive(!gunUICanvas.activeSelf);
		}
	}
}
