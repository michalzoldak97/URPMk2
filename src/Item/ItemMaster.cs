using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace URPMk2
{
    public class ItemMaster : MonoBehaviour
    {
        public delegate void ItemInteractionsEventhandler(Transform origin);
        public event ItemInteractionsEventhandler EventInteractionRequested;

        public void CallEventInteractionReqiested(Transform origin)
        {
            EventInteractionRequested?.Invoke(origin);
        }
    }
}