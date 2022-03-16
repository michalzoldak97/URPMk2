using UnityEngine;

namespace URPMk2
{
	public class PlayerInventoryMaster : MonoBehaviour, IInventoryMaster
	{
		public delegate void ItemEventHandler(Transform item);
		public event ItemEventHandler EventItemPickUp;

		public void CallEventItemPickUp(Transform item)
        {
			EventItemPickUp?.Invoke(item);
		}
	}
}
