using System.Collections.Generic;
using UnityEngine;

namespace URPMk2
{
	public class PlayerAmmo : MonoBehaviour
	{
		public Dictionary<string, int> playerAmmoStore { get; private set; }
		private PlayerMaster playerMaster;
		private void SetInit()
		{
			playerMaster = GetComponent<PlayerMaster>();
		}
        private void Start()
        {
			playerAmmoStore = new Dictionary<string, int>();

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
		private void ChangePlayerAmmo(string ammoCode, int amount, WeaponAmmo origin)
        {
			if (!playerAmmoStore.ContainsKey(ammoCode))
				return;

			if (amount > 0)
			{
				playerAmmoStore[ammoCode] += amount;
				return;
			}

			int availableAmount = playerAmmoStore[ammoCode] + amount >= 0 ? 
				-amount : 
				playerAmmoStore[ammoCode];

			playerAmmoStore[ammoCode] -= availableAmount;

			origin.ChangeAmmo(availableAmount);
		}
	}
}
