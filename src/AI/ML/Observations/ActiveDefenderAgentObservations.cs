using UnityEngine;

namespace URPMk2
{
	public class ActiveDefenderAgentObservations : IMLAgentObservations
    {
        public float NumOfVisibleEnemies { get; set; }
        public Vector3 AgentMapPosition { get; set; }
        public Vector3 SpottedEnemyMapPosition { get; set; }
        public Vector3 EnemyMapPosition { get; set; }
    }
}
