using UnityEngine;
using UnityEngine.InputSystem;

namespace URPMk2
{
	public interface IActionMapChangeSensitive
	{
		public void InputMapChange(InputActionMap actionMapToSet);
	}
}
