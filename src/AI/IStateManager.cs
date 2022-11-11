using UnityEngine;
using UnityEngine.AI;

namespace URPMk2
{
	public interface IStateManager
	{
		public float GetCheckRate();

        public FSMSettingsSO GetFSMSettings();
        public void SetWaypoints(Transform[] waypoints);

    }
}
