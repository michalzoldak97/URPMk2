using UnityEngine;

namespace URPMk2
{
	public struct VisibilityParamContainer
	{
		public float searchRange, highResSearchRange, highResSearchSqrRange;
		public LayerMask layersToSearch;
		public Transform origin;

		public VisibilityParamContainer(
			float searchRange, 
			float highResSearchRange,
			float highResSearchSqrRange, 
			LayerMask layersToSearch, 
			Transform origin)
		{
			this.searchRange = searchRange;
			this.highResSearchRange = highResSearchRange;
			this.highResSearchSqrRange = highResSearchSqrRange;
			this.layersToSearch = layersToSearch;
			this.origin = origin;
		}
	}
}
