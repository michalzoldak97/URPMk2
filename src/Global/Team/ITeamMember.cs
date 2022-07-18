using UnityEngine;

namespace URPMk2
{
	public interface ITeamMember
	{
		public Teams TeamID { get; }
		public Vector3 BoundsExtens { get; }
		public Transform ObjTransform { get; }
		public GameObject Object { get; }
	}
}
