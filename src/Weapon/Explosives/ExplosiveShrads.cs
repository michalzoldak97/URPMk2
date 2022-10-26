using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace URPMk2
{
	public class ExplosiveShrads : MonoBehaviour
	{
		private DamageInfo dmgInfo;
        private ExplosiveMaster explosiveMaster;
		private void SetInit()
		{
			explosiveMaster = GetComponent<ExplosiveMaster>();
            dmgInfo = new DamageInfo(DamageType.Gun, transform);
        }
		
		private void OnEnable()
		{
			SetInit();
			explosiveMaster.EventExplode += ReleaseShrads;
		}
		
		private void OnDisable()
		{
            explosiveMaster.EventExplode -= ReleaseShrads;
        }
		private void ApplyForceAndDamage(float forceAndDamage, float shradRange, Vector3 pos, Transform target, ExplosiveSettings e)
		{
            if (target.GetComponent<Rigidbody>() != null)
                target.GetComponent<Rigidbody>().AddExplosionForce(forceAndDamage, pos, shradRange);

			dmgInfo.dmg = forceAndDamage;
			dmgInfo.pen = Random.Range(e.minMaxShradPenetration[0], e.minMaxShradPenetration[1]);
            dmgInfo.toDmg = target;
			dmgInfo.origin = explosiveMaster.damageOrigin;

            GlobalDamageMaster.DamageObj(dmgInfo);
        }
		private void ReleaseShrads()
		{
			Vector3 myPos = transform.position;
            ExplosiveSettings e = explosiveMaster.GetExplosiveSettings();
			for (int i = 0; i < e.shradNum; i++)
			{
				float shradRange = Random.Range(e.minMaxShradRange[0],
					e.minMaxShradRange[1]);

                if (Physics.Raycast(
					myPos,
                    Random.insideUnitSphere.normalized,
					out RaycastHit hit,
					shradRange,
					e.layersToHit))
				{
                    Transform target = hit.transform;
                    if (!((e.layersToDamage.value & (1 << target.gameObject.layer)) > 0))
						continue;

                    float forceAndDamage = Random.Range(e.minMaxShradDamage[0], 
						e.minMaxShradDamage[1]);
					ApplyForceAndDamage(forceAndDamage, shradRange, myPos, target, e);
				}
			}
		}
	}
}
