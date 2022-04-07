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
		public static Vector3 GetVector3FromFloat(float[] toSet)
        {
			if (toSet.Length != 3)
				return Vector3.zero;
			return new Vector3(toSet[0], toSet[1], toSet[2]);
        }
	}
}
