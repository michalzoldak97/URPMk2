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
            }
			return false;
        }
		public static bool IsVisibleSingle(VisibilityParamContainer origin, Vector3 checkDir, Transform target)
        {
			if (Physics.Raycast(
				origin.origin.position,
				checkDir,
				out RaycastHit hit,
				origin.searchRange,
				origin.layersToSearch))
			{
				if (hit.transform == target)
					return true;
			}
			return false;
        }
		public static bool IsVisibleCorners(VisibilityParamContainer origin, Transform target, Vector3 targetBounds)
        {
			Vector3 oPos = origin.origin.position;
			Vector3 targetCenter = target.position;
			Vector3 cornerPos = targetCenter;

			// top center
			cornerPos.y += targetBounds.y;
			if (CheckCorner(oPos, (cornerPos - oPos), origin.highResSearchRange, origin.layersToSearch, target))
				return true;

			// centre front
			cornerPos.y = targetCenter.y;
			cornerPos += target.forward * targetBounds.z;
			if (CheckCorner(oPos, (cornerPos - oPos), origin.highResSearchRange, origin.layersToSearch, target))
				return true;

			// centre rear
			cornerPos = targetCenter - (target.forward * targetBounds.z);
			if (CheckCorner(oPos, (cornerPos - oPos), origin.highResSearchRange, origin.layersToSearch, target))
				return true;

			// center +x
			cornerPos = targetCenter + (target.right * targetBounds.x);
			if (CheckCorner(oPos, (cornerPos - oPos), origin.highResSearchRange, origin.layersToSearch, target))
				return true;

			// center -x
			cornerPos = targetCenter - (target.right * targetBounds.x);
			if (CheckCorner(oPos, (cornerPos - oPos), origin.highResSearchRange, origin.layersToSearch, target))
				return true;

			return false;
		}
	}
}
