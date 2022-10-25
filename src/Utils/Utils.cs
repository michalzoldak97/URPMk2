using System.Collections.Generic;
using UnityEngine;

namespace URPMk2
{
	public static class Utils
	{
		public static WaitForSeconds waitForEndOfFrame = new WaitForEndOfFrame();
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
		public static Vector3 GetAbsVector3(Vector3 toAbs)
        {
			toAbs.x = Mathf.Abs(toAbs.x);
			toAbs.y = Mathf.Abs(toAbs.y);
			toAbs.z = Mathf.Abs(toAbs.z);
			return toAbs;
		}
		public static int Compare(Dictionary<string, int> key, string x, string y)
		{
			return key[x].CompareTo(key[y]);
		}
		public static void DrawRay(Vector3 start, Vector3 dir, float len, float t)
        {
			Debug.DrawRay(start, dir * len, Color.red, t);
        }
	}
}
