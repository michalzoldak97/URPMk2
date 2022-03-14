using UnityEngine;

namespace URPMk2
{
	public class PlayerInventoryMaster : MonoBehaviour
	{
		public delegate void ItemEventhandler(Transform item);
		public event ItemEventhandler EventPickUpReqiested;
	}
}
