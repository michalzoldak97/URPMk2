using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace URPMk2
{
    public class ItemMaster : MonoBehaviour, IItemMaster
    {
        [SerializeField] private ItemSettings itemSettings;
        public bool isSelectedOnParent { get; set; }
        public ItemSettings GetItemSettings() { return itemSettings; }

        public delegate void ItemInteractionsEventhandler(Transform origin);
        public event ItemInteractionsEventhandler EventInteractionRequested;
        public event ItemInteractionsEventhandler EventItemPickedUp;
        public event ItemInteractionsEventhandler EventItemThrow;

        public delegate void ItemInventoryEventhandler();
        public event ItemInventoryEventhandler EventActivateOnParent;
        public event ItemInventoryEventhandler EventDisableOnParent;

        public void CallEventInteractionRequested(Transform origin)
        {
            EventInteractionRequested?.Invoke(origin);
        }
        public void CallEventItemPickedUp(Transform origin)
        {
            EventItemPickedUp?.Invoke(origin);
        }
        public void CallEventItemThrow(Transform origin)
        {
            EventItemThrow?.Invoke(origin);
        }
        public void CallEventActivateOnParent()
        {
            EventActivateOnParent?.Invoke();
        }
        public void CallEventDisableOnParent()
        {
            EventDisableOnParent?.Invoke();
        }
    }
}