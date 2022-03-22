using UnityEngine;

namespace URPMk2
{
	public class ItemLayer : MonoBehaviour
	{
		private int originalLayer;
		private ItemMaster itemMaster;
		private void SetInit()
		{
			itemMaster = GetComponent<ItemMaster>();
			originalLayer = gameObject.layer;
		}
		
		private void OnEnable()
		{
			SetInit();
			itemMaster.EventItemPickedUp += ChangeOnPickUp;
			itemMaster.EventItemThrow += ChangeOnThrow;
		}
		
		private void OnDisable()
		{
			itemMaster.EventItemPickedUp -= ChangeOnPickUp;
			itemMaster.EventItemThrow -= ChangeOnThrow;
		}
		private void SetLayer(int toSet)
        {
			gameObject.layer = toSet;
			foreach (Transform child in transform)
			{
				child.gameObject.layer = toSet;
			}
		}
		private void ChangeOnPickUp(Transform origin)
        {
			int layerToSet = LayerMask.NameToLayer(itemMaster.GetItemSettings().toLayerName);
			SetLayer(layerToSet);
		}
		private void ChangeOnThrow(Transform origin)
        {
			SetLayer(originalLayer);
		}
	}
}
