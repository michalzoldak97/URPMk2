using UnityEngine;

namespace URPMk2
{
	public class ExplosiveStartFuseTrigger : MonoBehaviour
	{
		private void Start()
		{
			GetComponent<ExplosiveMaster>().CallEventTriggerFuse();
		}
	}
}
