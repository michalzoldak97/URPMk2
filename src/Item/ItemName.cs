using UnityEngine;

namespace URPMk2
{
	public class ItemName : MonoBehaviour
	{
        private void Start()
        {
            gameObject.name = GetComponent<ItemMaster>().GetItemSettings().toItemName;
        }
    }
}
