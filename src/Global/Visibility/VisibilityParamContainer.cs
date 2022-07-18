using UnityEngine;

namespace URPMk2
{
	public class VisibilityParamContainer
	{
		public float searchRange, highResSearchSqrRange;
		public LayerMask layersToSearch;
		public Transform origin;

		public VisibilityParamContainer(
			float searchRange, 
			float highResSearchSqrRange, 
			LayerMask layersToSearch, 
			Transform origin)
		{
			this.searchRange = searchRange;
			this.highResSearchSqrRange = highResSearchSqrRange;
			this.layersToSearch = layersToSearch;
			this.origin = origin;
		}
	}
}
