using System.Collections;
using UnityEngine;

namespace URPMk2
{
	public class ExplosiveTimeFuse : MonoBehaviour
	{
		private ExplosiveMaster explosiveMaster;
		private void SetInit()
		{
			explosiveMaster = GetComponent<ExplosiveMaster>();
        }
		
		private void OnEnable()
		{
			SetInit();
			explosiveMaster.EventTriggerFuse += StartTriggerTimeFuse;
		}
		
		private void OnDisable()
		{
            explosiveMaster.EventTriggerFuse -= StartTriggerTimeFuse;
        }
		private void StartTriggerTimeFuse()
		{
			StartCoroutine(TriggerTimeFuse());
		}
		private IEnumerator TriggerTimeFuse()
		{
			yield return new WaitForSeconds(
				explosiveMaster.GetExplosiveSettings().timeToExplode);

			if (explosiveMaster != null)
				explosiveMaster.CallEventExplode();
        }
	}
}
