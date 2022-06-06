using UnityEngine;

namespace URPMk2
{
	public interface ITeamMember
	{
		public float DistTo { get; set; }
		public Teams TeamID { get; }
		public Vector3 BoundsExtens { get; }
		public GameObject Object { get; }
		public Vector3 GetPos();
	}
}
