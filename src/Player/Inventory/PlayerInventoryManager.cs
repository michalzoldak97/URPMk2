using System.Collections.Generic;
using UnityEngine;

namespace URPMk2
{
	public class PlayerInventoryManager : MonoBehaviour
	{
		private PlayerInventoryMaster inventoryMaster;
		private List<Transform> inventoryItems = new List<Transform>();
		public List<Transform> GetInventoryItems() { return inventoryItems; }
		private void SetInit()
		{
			inventoryMaster = GetComponent<PlayerInventoryMaster>();
		}
		
		private void OnEnable()
		{
			SetInit();
			inventoryMaster.EventItemPickUp += AddItem;
		}
		
		private void OnDisable()
		{
			inventoryMaster.EventItemPickUp -= AddItem;
		}
		private void AddItem(Transform item)
        {
			if (!inventoryItems.Contains(item))
			{
				inventoryItems.Add(item);
				inventoryMaster.CallEventItemPlaced(item);
			}
        }
	}
}
