using UnityEngine;

namespace URPMk2
{
	public class WeaponGunHitForce : MonoBehaviour
	{
		private float hitForce;
		private Transform myTransform;
        private WeaponMaster weaponMaster;
        private void SetInit()
		{
			myTransform = transform;
            weaponMaster = GetComponent<WeaponMaster>();
			hitForce = weaponMaster.GetWeaponSettings().damageSettings.hitForce;
        }
		
		private void OnEnable()
		{
			SetInit();
			weaponMaster.EventHitByGun += ApplyHitForce;
		}
		
		private void OnDisable()
		{
            weaponMaster.EventHitByGun -= ApplyHitForce;
        }

		private void ApplyHitForce(RaycastHit hit)
		{
            if (hit.transform.GetComponent<Rigidbody>() != null)
				hit.transform.GetComponent<Rigidbody>().AddForce(myTransform.forward * hitForce, ForceMode.Impulse);
        }
	}
}
