using UnityEngine;

namespace URPMk2
{
	public class WeaponHGShoot : MonoBehaviour
	{
		private Transform myTransform;
		private WeaponMaster weaponMaster;
		private void SetInit()
		{
			myTransform = transform;
            weaponMaster = GetComponent<WeaponMaster>();
        }
		
		private void OnEnable()
		{
			SetInit();
			weaponMaster.EventThrowGrenade += OnThrowGrenade;
		}
		
		private void OnDisable()
		{
            weaponMaster.EventThrowGrenade -= OnThrowGrenade;
        }
		private void ThrowGrenade(float force, float angle)
		{
            Vector3 dRotAngles = myTransform.localEulerAngles;
            if (dRotAngles.x > 180)
                dRotAngles.x -= 360;

            dRotAngles.x = angle;

            myTransform.localRotation = Quaternion.Euler(dRotAngles);
            GameObject projectile = Instantiate(
                    weaponMaster.GetWeaponSettings().projectileObj,
                    myTransform.position,
                    myTransform.rotation
                    );
            projectile.GetComponent<Rigidbody>().AddForce(myTransform.forward * force, ForceMode.Impulse);

            weaponMaster.CallEventShoot();
        }
        private async void ResetShootState()
        {
            await System.TimeSpan.FromSeconds(60f / 
                weaponMaster.GetWeaponSettings().gunSettings.shootRate);
            weaponMaster.isShootState = false;
        }
        private void OnThrowGrenade(float force, float angle)
		{
            if (weaponMaster.isReloading ||
                !weaponMaster.isWeaponLoaded ||
                weaponMaster.isShootingBurst)
                return;

            weaponMaster.isShootState = true;

            ThrowGrenade(force, angle);
            ResetShootState();
        }
    }
}
