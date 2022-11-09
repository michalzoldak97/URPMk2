using UnityEngine;

namespace URPMk2
{
	public static class CurrentMainCameraManager
	{
		public static Transform CurrentCamera { get; private set; }

		public static void SetCurrentCamera(Transform toSet)
		{
			if (toSet.GetComponent<Camera>() == null)
				return;

			CurrentCamera = toSet;
		}
		public static void RestoreMainCamera()
		{
			CurrentCamera = Camera.main.transform;
        }
	}
}
