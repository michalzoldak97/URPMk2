using System.Collections.Generic;
using UnityEngine;

namespace URPMk2
{
	public class NPCAmmo : MonoBehaviour
	{
		public Dictionary<string, int> NpcAmmoStore { get; private set; }
		private string primaryAmmoCode;
		private NPCMaster npcMaster;
		private void SetInit()
		{
			NpcAmmoStore = new Dictionary<string, int>();
			npcMaster = GetComponent<NPCMaster>();
		}
        private void Start()
        {
			foreach (NPCAmmoSlot ammoSlot in npcMaster.GetFSMSettings().npcAmmoStore)
			{
				if (ammoSlot.isPrimary)
					primaryAmmoCode = ammoSlot.ammoCode;

				NpcAmmoStore.Add(ammoSlot.ammoCode, ammoSlot.ammoQuantity);
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
			if (!NpcAmmoStore.ContainsKey(ammoCode))
				return;

			if (amount > 0)
			{
				NpcAmmoStore[ammoCode] += amount;
				npcMaster.CallEventAmmoRecovered();
				return;
			}

			int availableAmount = NpcAmmoStore[ammoCode] + amount >= 0 ? -amount :
				NpcAmmoStore[ammoCode];

			if (ammoCode == primaryAmmoCode &&
				availableAmount < 1)
				npcMaster.CallEventAmmoFinished();

			NpcAmmoStore[ammoCode] -= availableAmount;

			origin.ChangeAmmo(availableAmount);
		}
	}
}
