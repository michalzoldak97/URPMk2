using UnityEngine;

namespace URPMk2
{
	public interface INPCWeaponController
	{
		public void LaunchAtack(Transform origin);
		public void LaunchHGAttack(float force, float angle, Transform origin);
	}
}
