using UnityEngine;

namespace URPMk2
{
	public class TeamMember : MonoBehaviour, ITeamMember
	{
		[SerializeField] private int teamID;
		public Teams TeamID { get; set; }
		public Vector3 BoundsExtens { get; private set; }
		public GameObject Object { get; private set; }
		private Transform myTransform;

		public Vector3 GetPos()
        {
			return myTransform.position;
		}

		// iterates over all gameObject colliders
		// returns max bounds - will be used to detect object with box cast
		private Vector3 GetBoundExtents()
        {
			float maxX = 0, maxY = 0, maxZ = 0;
			foreach (Collider col in gameObject.GetComponents<Collider>())
            {
				if (col.bounds.extents.x > maxX)
					maxX = col.bounds.extents.x;
				if (col.bounds.extents.y > maxY)
					maxY = col.bounds.extents.y;
				if (col.bounds.extents.z > maxZ)
					maxZ = col.bounds.extents.z;
			}

			return new Vector3(maxX, maxY, maxZ);
        }

		private void SetInit()
		{
			myTransform = transform;
			BoundsExtens = GetBoundExtents();
			Object = gameObject;
			// To DO figure it out how to get this from settings...
			TeamID = (Teams) teamID;
			TeamMembersManager.RegisterInTeamMembers(this);
		}
		
		private void OnEnable()
		{
			SetInit();
		}
		
		private void OnDisable()
		{
			TeamMembersManager.RemoveFromTeamMembers(this);
		}
	}
}
