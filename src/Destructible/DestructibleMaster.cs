using UnityEngine;

namespace URPMk2
{
	public class DestructibleMaster : MonoBehaviour
	{
		[SerializeField] private DestructibleSettingsSO destructibleSettings;
		public DestructibleSettingsSO GetDestructibleSettings() 
		{ 
			return destructibleSettings;  
		}
		public delegate void DestructibleEventHandler();

		public event DestructibleEventHandler EventInitializeDestructibleEffect;
		public event DestructibleEventHandler EventDeactivateDestructibleEffect;

		public void CallEventInitializeDestructibleEffect()
        {
			EventInitializeDestructibleEffect?.Invoke();
		}
		public void CallEventDeactivateDestructibleEffect()
        {
			EventDeactivateDestructibleEffect?.Invoke();
		}
	}
}
