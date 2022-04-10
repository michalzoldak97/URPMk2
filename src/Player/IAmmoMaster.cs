using UnityEngine;

namespace URPMk2
{
	public interface IAmmoMaster
	{
		public void CallEventAmmoChange(string ammoCode, int amount, WeaponAmmo origin);
	}
}
