using UnityEngine;

namespace URPMk2
{
	[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/NPCWeaponSettings", order = 7)]
	public class NPCWeaponSO : ScriptableObject
	{
		public float[] xMinMaxRotation;
		public float[] yMinMaxRotation;
	}
}
