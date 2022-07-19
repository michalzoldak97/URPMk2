using UnityEngine;

namespace URPMk2
{
	public static class VisibilityCalculator
	{
		private static bool CheckCorner(Vector3 originPos, Vector3 dir, float range, LayerMask sightlayers, Transform target)
        {
			if (Physics.Raycast(originPos, dir, out RaycastHit hit, range, sightlayers))
            {
				if (hit.transform == target)
					return true;
				else
					return false;
            }
			return false;
        }
		public static bool IsVisibleSingle(VisibilityParamContainer origin, ITeamMember target)
        {
			Vector3 oPos = origin.origin.position;
			Vector3 checkDir = target.ObjTransform.position - oPos;

			Debug.DrawRay(oPos, checkDir * origin.searchRange, Color.blue, 1.8f); // debug

			if (Physics.Raycast(
				oPos,
				checkDir, 
				out RaycastHit hit, 
				origin.searchRange, 
				origin.layersToSearch))
            {
				if (hit.transform == target.ObjTransform)
                {
					Debug.Log("I can see u: " + hit.transform.name + "  bnds: " + target.BoundsExtens);
					return true;
				}

				if (checkDir.sqrMagnitude < origin.highResSearchSqrRange)
                {
					Vector3 targetCenter = target.ObjTransform.position;
					Vector3 cornerPos = targetCenter;
					Vector3 targetBounds = target.BoundsExtens;
					// top center
					cornerPos.y += targetBounds.y;
					Debug.DrawRay(oPos, (cornerPos - oPos) * origin.highResSearchRange, Color.red, 1.8f);
					if (CheckCorner(oPos, (cornerPos - oPos), origin.highResSearchRange, origin.layersToSearch, target.ObjTransform))
						return true;

					// center +x
					cornerPos.y = targetCenter.y;
					cornerPos.x += targetBounds.x;
					Debug.DrawRay(oPos, (cornerPos - oPos) * origin.highResSearchRange, Color.red, 1.8f);
					if (CheckCorner(oPos, (cornerPos - oPos), origin.highResSearchRange, origin.layersToSearch, target.ObjTransform))
						return true;

					// center -x
					cornerPos.x -= targetBounds.x * 2.0f;
					Debug.DrawRay(oPos, (cornerPos - oPos) * origin.highResSearchRange, Color.red, 1.8f);
					if (CheckCorner(oPos, (cornerPos - oPos), origin.highResSearchRange, origin.layersToSearch, target.ObjTransform))
						return true;

					// center +z
					cornerPos.x = targetCenter.x;
					cornerPos.z += targetBounds.z;
					Debug.DrawRay(oPos, (cornerPos - oPos) * origin.highResSearchRange, Color.red, 1.8f);
					if (CheckCorner(oPos, (cornerPos - oPos), origin.highResSearchRange, origin.layersToSearch, target.ObjTransform))
						return true;

					// center -z
					cornerPos.z -= targetBounds.z * 2.0f;
					Debug.DrawRay(oPos, (cornerPos - oPos) * origin.highResSearchRange, Color.red, 1.8f);
					if (CheckCorner(oPos, (cornerPos - oPos), origin.highResSearchRange, origin.layersToSearch, target.ObjTransform))
						return true;
				}
				return false;
            }
			return false;
        }
	}
}
