using UnityEngine;

namespace URPMk2
{
	[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/FSMSettings", order = 6)]
	public class FSMSettingsSO : ScriptableObject
	{
		public bool shouldInformAllies; // will inform about enemies found
		public Teams teamID;
		public int requiredDetectionCount; // detections to confirm target as an enemy
		public int sightRange;
		public int highResDetectionRange;
		public int fleeAttackRangePow;
		public int informAlliesRangePow; 
		public int enemiesBufferSize;
		public int attackRange;
		public int targetLostDetections;
		public Teams[] teamsToAttack; // order is team importance
		public Teams[] healthRecoverTeams;
		public Teams[] ammoRecoverTeams;
		public Teams[] teamsToDefend;
		public Teams[] teamsToAlert;
		public float checkRate;
		public float checkRateOffset;
		public float recoverFromDmgTime;
		public float minDotProd;
		public float informAlliesPeriod;
		public float rotationAngularSpeed;
		public float rotationsPerCycle;
		public float shiftRange;
		public float nmaStoppingDistance;
		public LayerMask sightLayers;
		public LayerMask enemyLayers;
		public LayerMask friendlyLayers;
		public NPCAmmoSlot[] npcAmmoStore;
	}
	[System.Serializable]
	public class NPCAmmoSlot
	{
		public bool isPrimary;
		public string ammoCode;
		public int ammoQuantity;
	}
}
