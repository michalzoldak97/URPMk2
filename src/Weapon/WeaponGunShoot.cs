using UnityEngine;

namespace URPMk2
{
	public class WeaponGunShoot : MonoBehaviour
	{
		private float recoil, shootRange;
		private LayerMask layersToHit;
		private Vector3 shootStartPos;
		private Transform myTransform;
		private WeaponMaster weaponMaster;
		private void SetInit()
		{
			weaponMaster = GetComponent<WeaponMaster>();
		}
        private void Start()
        {
			myTransform = transform;
			GunSettings gunSettings = weaponMaster.GetWeaponSettings().gunSettings;
			recoil = gunSettings.recoil;
			shootRange = gunSettings.shootRange;
			layersToHit = gunSettings.layersToHit;
			shootStartPos = Utils.GetVector3FromFloat(gunSettings.shootStartPos);
		}
        private void OnEnable()
		{
			SetInit();
			weaponMaster.EventShoot += OnShoot;
		}
		
		private void OnDisable()
		{
			weaponMaster.EventShoot -= OnShoot;
		}
		private void OnShoot()
        {
            if (Physics.Raycast(
				myTransform.TransformPoint(shootStartPos),
                myTransform.TransformDirection(
                    Random.Range(-recoil, recoil),
                    Random.Range(-recoil, recoil),
                    shootStartPos.z), 
				out RaycastHit hit, 
				shootRange,
				layersToHit))
            {
				weaponMaster.CallEventHitByGun(hit);
            }
        }
	}
}
