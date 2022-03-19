using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace URPMk2
{
    public class ItemMaster : MonoBehaviour, IItemMaster
    {
        [SerializeField] private ItemSettings itemSettings;
        public ItemSettings GetItemSettings() { return itemSettings; }

        public delegate void ItemInteractionsEventhandler(Transform origin);
        public event ItemInteractionsEventhandler EventInteractionRequested;
        public event ItemInteractionsEventhandler EventItemPickedUp;
        public event ItemInteractionsEventhandler EventItemThrow;

        public void CallEventInteractionRequested(Transform origin)
        {
            EventInteractionRequested?.Invoke(origin);
        }
        public void CallEventItemPickedUp(Transform origin)
        {
            EventItemPickedUp?.Invoke(origin);
        }
        public void CallEventEventItemThrow(Transform origin)
        {
            EventItemThrow?.Invoke(origin);
        }
    }
}