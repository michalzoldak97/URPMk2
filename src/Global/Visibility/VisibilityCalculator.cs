using UnityEngine;

namespace URPMk2
{
	public static class VisibilityCalculator
	{
		// dist, layer
		public static bool IsVisibleSingle(VisibilityParamContainer origin, ITeamMember target)
        {
			Vector3 oPos = origin.origin.position;
			Vector3 checkDir = target.ObjTransform.position - oPos;
			if (Physics.Raycast(
				oPos,
				checkDir, 
				out RaycastHit hit, 
				origin.searchRange, 
				origin.layersToSearch))
            {
				if (hit.transform == target.ObjTransform)
                {
					Debug.Log("I can see u: " + hit.transform.name);
					return true;
				}

				if (checkDir.sqrMagnitude < origin.highResSearchSqrRange)
                {
					Debug.Log("Distance is lower");

					RaycastHit[] cols = Physics.BoxCastAll(oPos, target.BoundsExtens, checkDir, target.ObjTransform.rotation, origin.searchRange, origin.layersToSearch);
					int colsLen = cols.Length;

					for (int i = 0; i < colsLen; i++)
                    {
						Debug.Log(cols[i].transform.name + "is in box range");
                    }
					return false;
                }
				Debug.Log("Distance is higher");
				return false;
            }
			return false;
        }
	}
}
