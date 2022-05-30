using UnityEngine;

namespace URPMk2
{
	public class FSMMaster : MonoBehaviour
	{
		[SerializeField] private FSMSettingsSO fsmSettings;
		public FSMSettingsSO GetFSMSettings() { return fsmSettings; }


	}
}
