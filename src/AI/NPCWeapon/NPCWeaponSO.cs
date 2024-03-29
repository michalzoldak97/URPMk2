using UnityEngine;

namespace URPMk2
{
	[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/NPCWeaponSettings", order = 7)]
	public class NPCWeaponSO : ScriptableObject
	{
		public float minDotProd;
		public float[] xMinMaxRotation;
		public float[] yMinMaxRotation;
		public NPCWeaponGreanadeSettings greanadeSettings;
    }
	[System.Serializable]
	public struct NPCWeaponGreanadeSettings
	{
		public bool isRandomAttack;
		public int randAttackChance; // max 10
		public float horizontalObstacleCheckRadius;
        public float verticalObstacleCheckRadius;
		public float grenadeThrowRange;
		public float checkOffset;
        public float[] forceEquation;
		public float[] angleEquation;
    }
}
