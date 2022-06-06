using System.Collections.Generic;
using UnityEngine;

namespace URPMk2
{
	[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/FSMSettings", order = 5)]
	public class FSMSettingsSO : ScriptableObject
	{
		// public Teams TeamID { get { return (Teams) teamID;  } set { } }
		public int teamID;
		public int requiredDetectionCount;
		public int sightRange;
		public int highResDetectionRange;
		public int fleeRange;
		// public Teams[] teamsToAttack;
		public float checkRate;
		public float attackRate;
		public float recoverFromDmgTime;
		public string[] tagsToAttack;
		public LayerMask sightLayers;
		public LayerMask enemyLayers;
		public LayerMask friendlyLayers;
	}
}
