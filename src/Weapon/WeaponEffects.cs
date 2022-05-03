using System;
using UnityEngine;
using UnityEngine.VFX;
namespace URPMk2
{
	[Serializable]
	class MissingObjectException : Exception
	{
		public MissingObjectException() { }

		public MissingObjectException(string name)
			: base(String.Format("Fatal Error, {0} object is missing", name))
		{

		}
	}
	public class WeaponEffects : MonoBehaviour
	{
		[SerializeField] private VisualEffect shootEffect;
		private string[] hitEffectTags;
		private ObjectPool objectPool;
		private WeaponMaster weaponMaster;
		private void SetInit()
		{
			weaponMaster = GetComponent<WeaponMaster>();
		}
        private void Start()
        {
            try
            {
				objectPool = GameObject.FindGameObjectWithTag("SceneRoot").GetComponent<ObjectPool>();
			}
			catch
            {
				throw new MissingObjectException("Object Pooler");
			}
			hitEffectTags = weaponMaster.GetWeaponSettings().gunSettings.hitEffectTags;
		}
        private void OnEnable()
		{
			SetInit();
			weaponMaster.EventShoot += PlayShootEffect;
			weaponMaster.EventHitByGun += HandleGunHit;
		}
		
		private void OnDisable()
		{
			weaponMaster.EventShoot -= PlayShootEffect;
			weaponMaster.EventHitByGun -= HandleGunHit;
		}
		private void PlayShootEffect()
        {
			shootEffect.Play();
        }
		private void HandleGunHit(RaycastHit hit)
        {
			int hitLayer = hit.transform.gameObject.layer;
			if ((GameConfig.stoneLayers & (1 << hitLayer)) != 0)
				PlaceHitEffect(hit, hitEffectTags[0]);
			else if ((GameConfig.metalLayers & (1 << hitLayer)) != 0)
				PlaceHitEffect(hit, hitEffectTags[1]);
			else
				PlaceHitEffect(hit, hitEffectTags[0]);
		}
		private void PlaceHitEffect(RaycastHit hit, string hitTag)
        {
			GameObject eff = objectPool.GetObjectFromPool(hitTag);
			if (eff == null)
				return;

			eff.transform.position = hit.point;
			eff.transform.rotation = Quaternion.LookRotation(-hit.normal);
			eff.transform.SetParent(hit.transform);
			eff.SetActive(true);
        }
	}
}
