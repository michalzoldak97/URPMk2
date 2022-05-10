using UnityEngine;
using UnityEngine.VFX;
namespace URPMk2
{
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
            PooledObjectInstance effInstance = objectPool.GetObjectFromPool(hitTag);
            if (effInstance == null)
                return;

			Transform effTransform = effInstance.obj.transform;
			effTransform.position = hit.point;
			effTransform.rotation = Quaternion.LookRotation(-hit.normal);
			effTransform.SetParent(hit.transform);
			effInstance.obj.SetActive(true);
			effInstance.objBehavior.Activate();
		}
	}
}
