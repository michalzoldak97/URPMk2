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
		public float horizontalObstacleCheckRadius;
        public float verticalObstacleCheckRadius;
        public float[] minMaxThrowForce;
    }
}
