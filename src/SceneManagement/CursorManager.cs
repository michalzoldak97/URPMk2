using UnityEngine;

namespace URPMk2
{
	public static class CursorManager
	{
		public static void ToggleCursorState(bool toState)
        {
			if (toState)
            {
				Cursor.lockState = CursorLockMode.None;
			}
            else
            {
				Cursor.lockState = CursorLockMode.Locked;
			}
        }
	}
}
