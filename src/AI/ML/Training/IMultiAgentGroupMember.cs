using Unity.MLAgents;
using UnityEngine;

namespace URPMk2
{
	public interface IMultiAgentGroupMember
	{
		public int GroupID { get; set; }
		public Transform AgentTransform { get; set; }
		public Agent Agent { get; set; }

	}
}
