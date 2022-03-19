using UnityEngine;

namespace URPMk2
{
	[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ItemSettings", order = 2)]
	public class ItemSettings : ScriptableObject
	{
		public bool deactivateObjOnPickUp;
		public bool deactivateCollOnPickUp;
		public string toItemName;
		public string toLayerName;
		public float[] onPlayerPosition;
		public float[] onPlayerRotation;
	}
}
