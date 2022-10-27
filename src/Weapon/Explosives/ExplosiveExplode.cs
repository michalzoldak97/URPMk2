using System.Collections.Generic;
using UnityEngine;

namespace URPMk2
{
	public class ExplosiveExplode : MonoBehaviour
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
        }
		private void OnEnable()
		{
			SetInit();
			explosiveMaster.EventExplode += Explode;
        }
		
		private void OnDisable()
		{
            explosiveMaster.EventExplode -= Explode;
        }
        
        private bool IsVisibleDirect(float range, LayerMask checklayers, Vector3 pos, Vector3 checkDir, Transform target)
		{
			if (Physics.Raycast(pos, checkDir, out RaycastHit hit, range, checklayers))
			{
				if (hit.transform == target)
					return true;
			}
			return false;
		}
        private bool CheckCorner(Vector3 originPos, Vector3 dir, float range, LayerMask sightlayers, Transform target)
        {
            if (Physics.Raycast(originPos, dir, out RaycastHit hit, range, sightlayers))
            {
                if (hit.transform == target)
                    return true;
            }
            return false;
        }
        private bool IsVisibleCorners(float range, LayerMask checklayers, Vector3 oPos, Collider targetCol, Transform target)
		{
			Vector3 targetCenter = target.position;
			Vector3 cornerPos = targetCenter;
            Vector3 targetBounds = targetCol.bounds.extents * .99f;

			cornerPos.y += targetBounds.y;
            cornerPos += target.forward * targetBounds.z;
			if (CheckCorner(oPos, (cornerPos - oPos), range, checklayers, target))
				return true;

			// centre rear
            cornerPos = targetCenter - (target.forward * targetBounds.z);
			if (CheckCorner(oPos, (cornerPos - oPos), range, checklayers, target))
				return true;

			// center +x
            cornerPos = targetCenter + (target.right * targetBounds.x);
            if (CheckCorner(oPos, (cornerPos - oPos), range, checklayers, target))
				return true;

			// center -x
			cornerPos = targetCenter - (target.right * targetBounds.x);
            if (CheckCorner(oPos, (cornerPos - oPos), range, checklayers, target))
				return true;

			return false;
        }
		private bool IsTargetVisible(Vector3 pos, Collider targetCol, Transform target, ExplosiveSettings e)
		{
			if (IsVisibleDirect(
					e.expRadius,
					e.layersToHit,
					pos,
                    (target.position - pos).normalized,
					target
				))
				return true;
			if (IsVisibleCorners(
                    e.expRadius,
                    e.layersToHit,
                    pos,
					targetCol,
					target
                ))
				return true;

			return false;
		}
		private void ApplyImpact(Vector3 pos, Collider targetCol, ExplosiveSettings e)
		{
			if (!IsTargetVisible(pos, targetCol, targetCol.transform, e))
				return;

			float distToTarget = (targetCol.ClosestPointOnBounds(pos) - pos).sqrMagnitude;

			float realForce = distToTarget <= e.dmgThresholdPow ? e.expForce :
                e.expForce / (distToTarget / e.dmgThresholdPow);

			if (targetCol.GetComponent<Rigidbody>() != null)
				targetCol.transform.GetComponent<Rigidbody>().AddExplosionForce(realForce, pos, e.expRadius);

			if (!((e.layersToDamage.value & (1 << targetCol.transform.gameObject.layer)) > 0))
				return;

            dmgInfo.dmg = distToTarget <= e.dmgThresholdPow ? e.expDamage : 
				Mathf.Abs((1 - (distToTarget / (e.expRadius * e.expRadius))) * e.expDamage);
            dmgInfo.pen = e.expPenetration * (dmgInfo.dmg / e.expDamage);
            dmgInfo.toDmg = targetCol.transform;
			dmgInfo.origin = explosiveMaster.damageOrigin;

            GlobalDamageMaster.DamageObj(dmgInfo);
        }
		private void Explode()
		{
			Vector3 myPos = myTransform.position;
			ExplosiveSettings e = explosiveMaster.GetExplosiveSettings();
			int numColliders = Physics.OverlapSphereNonAlloc(myPos, e.expRadius, hitColliders, e.layersToHit);
			for (int i = 0; i < numColliders; i++)
			{
				ApplyImpact(myPos, hitColliders[i], e);
			}
        }
	}
}
