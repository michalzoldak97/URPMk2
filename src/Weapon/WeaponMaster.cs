using UnityEngine;

namespace URPMk2
{
	public class WeaponMaster : MonoBehaviour
	{
		[SerializeField] private WeaponSettingsSO weaponSettings;
		public WeaponSettingsSO GetWeaponSettings() { return weaponSettings; }

		public bool isAim;
		public bool isReloading;
		public delegate void WeaponInputEvenHandler();
		public event WeaponInputEvenHandler EventAimRequest;
		public event WeaponInputEvenHandler EventUnAim;

		public void CallEventAimRequest()
        {
			EventAimRequest?.Invoke();
		}
		public void CallEventUnAim()
        {
			EventUnAim?.Invoke();
		}
	}
}
