using UnityEngine;
using UnityEngine.VFX;
namespace URPMk2
{
	public class WeaponEffects : MonoBehaviour
	{
		[SerializeField] private VisualEffect effect;
		private WeaponMaster weaponMaster;
		private void SetInit()
		{
			weaponMaster = GetComponent<WeaponMaster>();
		}
		
		private void OnEnable()
		{
			SetInit();
			weaponMaster.EventShoot += PlayShootEffect;
		}
		
		private void OnDisable()
		{
			weaponMaster.EventShoot -= PlayShootEffect;
		}
		private void PlayShootEffect()
        {
			effect.Play();
        }
	}
}
