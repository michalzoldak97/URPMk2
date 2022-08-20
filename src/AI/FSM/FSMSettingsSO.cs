using UnityEngine;

namespace URPMk2
{
	[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/FSMSettings", order = 5)]
	public class FSMSettingsSO : ScriptableObject
	{
		public bool shouldInformAllies; // will inform about enemies found
		public Teams teamID;
		public int requiredDetectionCount; // detections to confirm target as an enemy
		public int sightRange;
		public int highResDetectionRange;
		public int fleeAttackRangePow;
		public int informAlliesRange; 
		public int informAlliesNum; // max number of allies to infomr about the threat
		public int enemiesBufferSize;
		public int attackRange;
		public int targetLostDetections;
		public Teams[] teamsToAttack; // order is team importance
		public float checkRate;
		public float checkRateOffset;
		public float attackRate;
		public float recoverFromDmgTime;
		public float minDotProd;
		public float informAlliesPeriod;
		public LayerMask sightLayers;
		public LayerMask enemyLayers;
		public LayerMask friendlyLayers;
	}
}
