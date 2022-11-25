using UnityEngine;

namespace URPMk2
{
	public class InterceptorAgentObservations : IMLAgentObservations
	{
		public Vector3 AgentMapPosition { get; set; }
		public Vector3 EnemyMapPosition { get; set; }
		public Vector3 SpottedEnemyMapPosition { get; set; }
    }
}
