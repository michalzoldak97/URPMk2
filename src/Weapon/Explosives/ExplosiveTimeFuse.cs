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
			explosiveMaster.EventTriggerFuse += TriggerTimeFuse;
		}
		
		private void OnDisable()
		{
            explosiveMaster.EventTriggerFuse -= TriggerTimeFuse;
        }
		private async void TriggerTimeFuse()
		{
			await System.TimeSpan.FromSeconds(
				explosiveMaster.GetExplosiveSettings().timeToExplode);

			if (explosiveMaster != null)
				explosiveMaster.CallEventExplode();
        }
	}
}
