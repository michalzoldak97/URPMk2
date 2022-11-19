using Unity.MLAgents;
using UnityEngine;

namespace URPMk2
{
	public class MultiAgentGroupMember : IMultiAgentGroupMember
    {
		public MultiAgentGroupMember (int ID, Transform t, Agent a)
		{
			GroupID = ID;
			AgentTransform = t;
			Agent = a;
		}
        public int GroupID { get; set; }
		public Transform AgentTransform { get; set; }
		public Agent Agent{ get; set; }
	}
}
