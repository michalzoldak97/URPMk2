using UnityEngine;

namespace URPMk2
{
	public class ExplosiveMaster : MonoBehaviour
	{
        [SerializeField] private ExplosiveSettings explosiveSettings;
        public ExplosiveSettings GetExplosiveSettings(){ return explosiveSettings; }

        public delegate void ExplosiveEventsHandler();
        public event ExplosiveEventsHandler EventTriggerFuze;
        public event ExplosiveEventsHandler EventExplode;

        public void CallEventTriggerFuze()
        {
            EventTriggerFuze?.Invoke();
        }
        public void CallEventExplode()
        {
            EventExplode?.Invoke();
        }
    }
}
