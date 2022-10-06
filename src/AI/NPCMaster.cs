using UnityEngine;

namespace URPMk2
{
	public class NPCMaster : MonoBehaviour
	{
		public delegate void NPCEnemyEventsHandler(Transform target);
		public event NPCEnemyEventsHandler EventAttackTarget;
		public event NPCEnemyEventsHandler EventAlertAboutEnemy;

		public void CallEventAttackTarget(Transform target)
        {
			EventAttackTarget?.Invoke(target);
			NPCWeaponShootFieldValidator val = GetComponent<NPCWeaponShootFieldValidator>();
			if (val.IsShootFieldClean(transform))
				Debug.Log("Ratatatatatatatatat");
			else
				Debug.Log("Need to change pos");
		}
		public void CallEventAlertAboutEnemy(Transform target)
        {
			EventAlertAboutEnemy?.Invoke(target);
		}
	}
}
