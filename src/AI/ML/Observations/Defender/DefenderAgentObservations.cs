using UnityEngine;

namespace URPMk2
{
	public class DefenderAgentObservations : IMLAgentObservations
    {
        public DefenderAgentObservations()
        {
            SpottedEnemyMapPositions = new Vector3[3];
        }
        public float NumOfVisibleEnemies { get; set; }
        public Vector3 CargoParentMapPosition { get; set; }
        public Vector3 AgentMapPosition { get; set; }
        public Vector3[] SpottedEnemyMapPositions { get; set; }
        public Vector3 EnemyMapPosition { get; set; }
    }
}
