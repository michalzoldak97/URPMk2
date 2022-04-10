using System.Collections.Generic;
using UnityEngine;

namespace URPMk2
{
	public class PlayerAmmo : MonoBehaviour
	{
		private Dictionary<string, int> playerAmmoStore = new Dictionary<string, int>();
		private PlayerMaster playerMaster;
		private void SetInit()
		{
			playerMaster = GetComponent<PlayerMaster>();
		}
        private void Start()
        {
            foreach (PlayerAmmoSlot ammoSlot in playerMaster.GetPlayerSettings().playerAmmoStore)
            {
				playerAmmoStore.Add(ammoSlot.ammoCode, ammoSlot.ammoQuantity);
            }
        }

        private void OnEnable()
		{
			SetInit();
			playerMaster.EventPlayerAmmoChange += ChangePlayerAmmo;
		}
		
		private void OnDisable()
		{
			playerMaster.EventPlayerAmmoChange -= ChangePlayerAmmo;
		}
		private void ChangePlayerAmmo(string ammoCode, int amount)
        {
			if (!playerAmmoStore.ContainsKey(ammoCode) ||
				playerAmmoStore[ammoCode] + amount < 0)
				return;

			playerAmmoStore[ammoCode] += amount;
		}
	}
}
