using UnityEngine;

namespace URPMk2
{
	public static class Utils
	{
		// will not work for 31!!!
		public static int GetLayerIndexFromSingleLayer(LayerMask layer)
        {
			return (int)(Mathf.Log(layer.value, 2));
		}
	}
}
