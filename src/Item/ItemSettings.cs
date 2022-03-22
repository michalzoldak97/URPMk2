using UnityEngine;
using UnityEngine.UI;

namespace URPMk2
{
	[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ItemSettings", order = 2)]
	public class ItemSettings : ScriptableObject
	{
		public bool deactivateObjOnPickUp;
		public bool deactivateCollOnPickUp;
		public int itemTypeID;
		public string toItemName;
		public string toLayerName;
		public float[] onPlayerPosition;
		public float[] onPlayerRotation;
		public Sprite itemIcon;
		public Sprite itemImage;
	}
}
