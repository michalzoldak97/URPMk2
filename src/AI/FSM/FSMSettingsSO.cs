using UnityEngine;

namespace URPMk2
{
	[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/FSMSettings", order = 5)]
	public class FSMSettingsSO : ScriptableObject
	{
		public int requiredDetectionCount;
		public int sightRange;
		public int detectBehindRange;
		public int fleeRange;
		public Teams[] teamsToAttack;
		public float checkRate;
		public float attackRate;
		public float recoverFromDmgTime;
		public LayerMask sightLayers;
		public LayerMask enemyLayers;
		public LayerMask friendlyLayers;
	}
}
