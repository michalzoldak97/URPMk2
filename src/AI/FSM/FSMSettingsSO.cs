using UnityEngine;

namespace URPMk2
{
	[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/FSMSettings", order = 5)]
	public class FSMSettingsSO : ScriptableObject, IAITeam
	{
		public Teams TeamID { get; set; }
		public int requiredDetectionCount;
		public int sightRange;
		public int detectBehindRange;
		public int fleeRange;
		public Teams[] teamsToAttack;
		public float checkRate;
		public float attackRate;
		public float recoverFromDmgTime;
		public float lookOffset;
		public LayerMask sightLayers;
		public LayerMask enemyLayers;
		public LayerMask friendlyLayers;
	}
}
