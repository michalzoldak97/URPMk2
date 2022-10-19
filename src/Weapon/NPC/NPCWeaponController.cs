using UnityEngine;

namespace URPMk2
{
	public class NPCWeaponController : MonoBehaviour, INPCWeaponController
	{
		private WeaponMaster weaponMaster;
		private void Start()
		{
			weaponMaster = GetComponent<WeaponMaster>();
		}
		public void LaunchAtack(Transform origin)
        {
			weaponMaster.SetDmgOrigin(origin);
			weaponMaster.CallEventPullTrigger();
		}
        public void LaunchHGAttack(float force, float angle, Transform origin) { }
    }
}
