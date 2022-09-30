using UnityEngine;

namespace URPMk2
{
	public class NPCMaster : MonoBehaviour
	{
		public delegate void NPCAtackEventsHandler(Transform target);
		public event NPCAtackEventsHandler EventAttackTarget;

		public void CallEventAttackTarget(Transform target)
        {
			EventAttackTarget?.Invoke(target);
		}
	}
}
