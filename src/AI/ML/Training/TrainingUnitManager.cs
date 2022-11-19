using System.Collections.Generic;
using UnityEngine;

namespace URPMk2
{
	public class TrainingUnitManager : MonoBehaviour
	{
		public delegate void TrainingUnitEventHandler();
		public event TrainingUnitEventHandler EventStartNewEpisode;
		public event TrainingUnitEventHandler EventEndEpisode;

		private Dictionary<int, TrainingMultiAgentGroupManager> multiAgentGroups;

		private void Start()
		{
			CallEventStartNewEpisode();
        }

		public void CallEventStartNewEpisode()
		{
			EventStartNewEpisode?.Invoke();
        }
        public void CallEventEndEpisode()
        {
            EventEndEpisode?.Invoke();
        }
    }
}
