using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace URPMk2
{
	public class UIGunButton : MonoBehaviour, IUIComponent
	{
		[SerializeField] private int itemID;
		public int GetItemID() { return itemID;  }
		[SerializeField] private TMP_Text itemName;
		[SerializeField] private Image itemImage;

		public void SetItemName(string name)
        {
			itemName.text = name;
        }
		public void SetItemImage(Sprite image)
        {
			itemImage.sprite = image;
        }
	}
}
