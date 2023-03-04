using UnityEngine;

namespace URPMk2
{
	public class WeaponPlayerLauncher : MonoBehaviour
	{
		[SerializeField] GameObject warhead;

		private WeaponMaster weaponMaster;
		private void SetInit()
		{
			weaponMaster = GetComponent<WeaponMaster>();
        }
		
		private void OnEnable()
		{
			SetInit();
			weaponMaster.EventShoot += LaunchMissile;
			weaponMaster.EventReload += OnReload;
		}
		
		private void OnDisable()
		{
            weaponMaster.EventShoot -= LaunchMissile;
            weaponMaster.EventReload -= OnReload;
        }
		private void LaunchMissile()
		{
			Vector3 startPos = transform.position + (transform.forward * 2);
			

            Instantiate(weaponMaster.GetWeaponSettings().projectileObj, startPos, transform.rotation);
			Debug.Log("Instantiating missile at pos: " + startPos + " parent pos is: " + transform.position);
			warhead.SetActive(false); // TODO: separate warhead controller for multi missiles
        }
		private void OnReload()
		{
            warhead.SetActive(true);
        }
    }
}
