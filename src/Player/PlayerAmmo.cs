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
			
		}
		
		private void OnDisable()
		{
			
		}
	}
}
