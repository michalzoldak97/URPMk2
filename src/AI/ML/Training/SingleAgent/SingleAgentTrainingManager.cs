using UnityEngine;

namespace URPMk2
{
	public class SingleAgentTrainingManager : MonoBehaviour
	{

		public delegate void SingleAgentTrainingEventHandler();
		public event SingleAgentTrainingEventHandler EventStartEpisode;
		public event SingleAgentTrainingEventHandler EventFinishEpisode;

        public delegate void SingleAgentEventHandler(int idx);
        public event SingleAgentEventHandler EventAgentDestroyed;


        public void CallEventStartEpisode()
		{
			EventStartEpisode?.Invoke();
        }
		public void CallEventFinishEpisode()
		{
            EventFinishEpisode?.Invoke();
        }
		public void CallEventAgentDestroyed(int idx)
		{
			EventAgentDestroyed?.Invoke(idx);
        }
    }
}
