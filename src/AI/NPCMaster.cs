using System.Collections.Generic;
using UnityEngine;

namespace URPMk2
{
	public class NPCMaster : MonoBehaviour, IAmmoMaster
	{

		[SerializeField] private FSMSettingsSO FSMSettings;
		public FSMSettingsSO GetFSMSettings() { return FSMSettings; }
        [SerializeField] private NPCWeaponSO weaponSettings;
		public NPCWeaponSO GetNPCWeaponSettings() { return weaponSettings; }
        public NPCLook NpcLook {get; private set;}
		public delegate void NPCEnemyEventsHandler(Transform target);
		public event NPCEnemyEventsHandler EventAttackTarget;
		public event NPCEnemyEventsHandler EventAlertAboutEnemy;

		public delegate void NPCAmmoEventsHandler(string ammoCode, int amount, WeaponAmmo origin);
		public event NPCAmmoEventsHandler EventAmmoChange;

		public delegate void NPCAmmoStateEventsHandler();
		public event NPCAmmoStateEventsHandler EventAmmoFinished;
		public event NPCAmmoStateEventsHandler EventAmmoRecovered;

		private Dictionary<string, int> npcAmmoStore;

        private void Start()
        {
			NpcLook = GetComponent<NPCLook>();
			npcAmmoStore = GetComponent<NPCAmmo>().NpcAmmoStore;
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
		public void CallEventAmmoFinished()
		{
			EventAmmoFinished?.Invoke();
		}
		public void CallEventAmmoRecovered()
		{
			EventAmmoRecovered?.Invoke();
		}
		public Dictionary<string, int> GetAmmoStore()
		{
			return npcAmmoStore;
		}
	}
}
