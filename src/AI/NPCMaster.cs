using System.Collections.Generic;
using UnityEngine;

namespace URPMk2
{
	public class NPCMaster : MonoBehaviour, IAmmoMaster
	{

		[SerializeField] private FSMSettingsSO FSMSettings;
		public FSMSettingsSO GetFSMSettings() { return FSMSettings; }
		public delegate void NPCEnemyEventsHandler(Transform target);
		public event NPCEnemyEventsHandler EventAttackTarget;
		public event NPCEnemyEventsHandler EventAlertAboutEnemy;

		public delegate void NPCAmmoEventsHandler(string ammoCode, int amount, WeaponAmmo origin);
		public event NPCAmmoEventsHandler EventAmmoChange;

		private Dictionary<string, int> npcAmmoStore;

        private void Start()
        {
			npcAmmoStore = GetComponent<NPCAmmo>().npcAmmoStore;
        }

        public void CallEventAttackTarget(Transform target)
        {
			EventAttackTarget?.Invoke(target);
		}
		public void CallEventAlertAboutEnemy(Transform target)
        {
			EventAlertAboutEnemy?.Invoke(target);
		}
		public void CallEventAmmoChange(string ammoCode, int amount, WeaponAmmo origin)
        {
			EventAmmoChange?.Invoke(ammoCode, amount, origin);
		}
		public Dictionary<string, int> GetAmmoStore()
		{
			return npcAmmoStore;
		}
	}
}
