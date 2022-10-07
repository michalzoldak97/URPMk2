using System.Collections.Generic;
using UnityEngine;

namespace URPMk2
{
	public class NPCAmmo : MonoBehaviour
	{
		public Dictionary<string, int> npcAmmoStore { get; private set; }
		private NPCMaster npcMaster;
		private void SetInit()
		{
			npcAmmoStore = new Dictionary<string, int>();
			npcMaster = GetComponent<NPCMaster>();
		}
        private void Start()
        {
			foreach (NPCAmmoSlot ammoSlot in npcMaster.GetFSMSettings().npcAmmoStore)
			{
				npcAmmoStore.Add(ammoSlot.ammoCode, ammoSlot.ammoQuantity);
			}
		}
        private void OnEnable()
		{
			SetInit();
			npcMaster.EventAmmoChange += ChangeAmmo;
		}
		
		private void OnDisable()
		{
			npcMaster.EventAmmoChange -= ChangeAmmo;
		}
		private void ChangeAmmo(string ammoCode, int amount, WeaponAmmo origin)
        {
			if (!npcAmmoStore.ContainsKey(ammoCode))
				return;

			if (amount > 0)
			{
				npcAmmoStore[ammoCode] += amount;
				return;
			}

			int availableAmount = npcAmmoStore[ammoCode] + amount >= 0 ?
				-amount :
				npcAmmoStore[ammoCode];

			npcAmmoStore[ammoCode] -= availableAmount;

			origin.ChangeAmmo(availableAmount);
		}
	}
}
