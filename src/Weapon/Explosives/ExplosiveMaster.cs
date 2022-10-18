using UnityEngine;

namespace URPMk2
{
	public class ExplosiveMaster : MonoBehaviour
	{
        [SerializeField] private ExplosiveSettings explosiveSettings;
        public ExplosiveSettings GetExplosiveSettings(){ return explosiveSettings; }

        public delegate void ExplosiveEventsHandler();
        public event ExplosiveEventsHandler EventTriggerFuse;
        public event ExplosiveEventsHandler EventExplode;

        public void CallEventTriggerFuse()
        {
            EventTriggerFuse?.Invoke();
        }
        public void CallEventExplode()
        {
            EventExplode?.Invoke();
        }
    }
}
