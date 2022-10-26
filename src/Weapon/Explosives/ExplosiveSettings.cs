using UnityEngine;

namespace URPMk2
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ExplosiveSettings", order = 8)]
    public class ExplosiveSettings : ScriptableObject
	{
		public float expDamage;
		public float expAbsDamage;
		public float dmgTresholdPow;
		public float expPenetration;
		public float expForce;
		public float expRadius;
		public float expAbsRadius; // apply dmg if a huge explosion is very close, ignore obstacles
		public float expDisableAfterSec;
		public float[] minMaxShradDamage;
		public float[] minMaxShradPenetration;
		public int maxHitColliders;
		public int timeToExplode;
		public int shradNum;
		public int[] minMaxShradRange;
		public LayerMask layersToHit;
		public LayerMask layersToDamage;
		public Vector3 effectOffset;
		public GameObject explosionEffect;
    }
}
