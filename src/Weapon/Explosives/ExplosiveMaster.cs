using UnityEngine;

namespace URPMk2
{
	public class ExplosiveMaster : MonoBehaviour
	{
        [SerializeField] private ExplosiveSettings explosiveSettings;
        public ExplosiveSettings GetExplosiveSettings(){ return explosiveSettings; }

        public Transform damageOrigin { get; private set; }

        public delegate void ExplosiveEventsHandler();
        public event ExplosiveEventsHandler EventTriggerFuse;
        public event ExplosiveEventsHandler EventExplode;

        public void SetDamageOrigin(Transform origin)
        {
            Debug.Log("Set origin as: " + origin.name);
            damageOrigin = origin;
        }

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
