
using UnityEngine;

namespace URPMk2
{
	public interface IStateManager	
    {
		public float GetCheckRate();

        public FSMSettingsSO GetFSMSettings();
        public Transform MyFollowTarget { get; set; }
    }
}
