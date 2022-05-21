using UnityEngine;

namespace URPMk2
{
	[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DestructionEffect", order = 5)]
	public class DestructibleSettingsSO : ScriptableObject
	{
		public bool isExplosion;
		public float explosionForce;
		public float explosionRadius;
		public float[] rotVec;
	}
}
