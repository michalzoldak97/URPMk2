using System.Collections.Generic;
using UnityEngine;

namespace URPMk2
{
	public class ExplosiveExplodeAbsolute : MonoBehaviour
	{
		private Transform myTransform;
		private Collider[] hitColliders;
		private DamageInfo dmgInfo;
		private ExplosiveMaster explosiveMaster;
		private void SetInit()
		{
			explosiveMaster = GetComponent<ExplosiveMaster>();
        }
		private void Start()
		{
			myTransform = transform;
            hitColliders = new Collider[explosiveMaster.GetExplosiveSettings().maxHitColliders];
            dmgInfo = new DamageInfo(DamageType.Explosion, myTransform);
			dmgInfo.dmg = e.expAbsDamage;
            dmgInfo.pen = e.expPenetration;
        }
		private void OnEnable()
		{
			SetInit();
			explosiveMaster.EventExplode += ExplodeAbsolute;
        }
		
		private void OnDisable()
		{
            explosiveMaster.EventExplode -= ExplodeAbsolute;
        }
		private void ApplyAbsDamage(Collider targetCol, ExplosiveSettings e)
		{
            dmgInfo.toDmg = targetCol.transform;
			dmgInfo.origin = explosiveMaster.damageOrigin;

            GlobalDamageMaster.DamageObj(dmgInfo);
		}
		private void ExplodeAbsolute()
		{
			ExplosiveSettings e = explosiveMaster.GetExplosiveSettings();
			int numColliders = Physics.OverlapSphereNonAlloc(myTransform.position, e.expAbsRadius, hitColliders, e.layersToDamage);
			for (int i = 0; i < numColliders; i++)
			{
				ApplyAbsDamage(hitColliders[i]);
			}
        }
    }
}