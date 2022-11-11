using UnityEngine;

namespace URPMk2
{
	public class MLStateManager : MonoBehaviour, IStateManager
    {
        [SerializeField] FSMSettingsSO FSMSettings;
        public FSMSettingsSO GetFSMSettings() { return FSMSettings; }
        public float GetCheckRate() { return checkRate; }
        public void SetWaypoints(Transform[] waypoints) { }

        private float checkRate;
    }
}
