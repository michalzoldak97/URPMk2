using UnityEngine;

namespace URPMk2
{
    // initializes static objects
	public class StaticInitializer : MonoBehaviour
	{
        private void Awake()
        {
            InputManager.Start();
            TeamMembersManager.Start();
        }
    }
}
