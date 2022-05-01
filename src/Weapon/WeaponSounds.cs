using UnityEngine;

namespace URPMk2
{
	public class WeaponSounds : MonoBehaviour
	{
		private AudioClip shootSound; // cashing because can be used frequently
		private AudioSource myAudioSource;
		private WeaponMaster weaponMaster;
		private void SetInit()
		{
			myAudioSource = GetComponent<AudioSource>();
			weaponMaster = GetComponent<WeaponMaster>();
		}
        private void Start()
        {
			shootSound = weaponMaster.GetWeaponSettings().shootSound;
		}
        private void OnEnable()
		{
			SetInit();
			weaponMaster.EventShoot += PlayShootSound;
			weaponMaster.EventStartReload += PlayReloadSound;
		}

		private void OnDisable()
		{
			weaponMaster.EventShoot -= PlayShootSound;
			weaponMaster.EventStartReload += PlayReloadSound;
		}
		private void PlayShootSound()
		{
			myAudioSource.PlayOneShot(shootSound);
		}
		private void PlayReloadSound()
        {
			myAudioSource.PlayOneShot(
				weaponMaster.GetWeaponSettings().reloadSound);
        }
	}
}
