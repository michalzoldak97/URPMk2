using UnityEngine;

namespace URPMk2
{
	public class ExplosiveSettings : MonoBehaviour
	{
		public float expDamage;
		public float dmgTresholdPow;
		public float expPenetration;
		public float expForce;
		public float expRadius;
		public int maxHitColliders;
		public LayerMask layersToHit;
		public LayerMask layersToDamage;
	}
}
