using System.Collections.Generic;
using UnityEngine;

namespace URPMk2
{
	public class PlayerInventoryMaster : MonoBehaviour, IInventoryMaster
	{
		public delegate void ItemEventHandler(Transform item);
		public event ItemEventHandler EventItemPickUp;
		public event ItemEventHandler EventItemPlaced;
		public event ItemEventHandler EventItemActivate;

		public void CallEventItemPickUp(Transform item)
        {
			EventItemPickUp?.Invoke(item);
		}
		public void CallEventItemPlaced(Transform item)
		{
			EventItemPlaced?.Invoke(item);
		}
		public void CallEventItemActivate(Transform item)
        {
			EventItemActivate?.Invoke(item);
		}

		public List<Transform> GetItemList()
        {
			if (GetComponent<PlayerInventoryManager>() != null)
				return GetComponent<PlayerInventoryManager>().GetInventoryItems();

			return new List<Transform>();
		}
	}
}
