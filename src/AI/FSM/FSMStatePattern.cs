using UnityEngine;

namespace URPMk2
{
	public class FSMStatePattern : MonoBehaviour
	{
		[SerializeField] private FSMSettingsSO FSMSettings;
		public FSMSettingsSO GetFSMSettings() { return FSMSettings; }
		public Vector3 LocationOfInterest { get; private set; }
		public Vector3 WanderTarget { get; private set; }
		public Transform MyFollowTarget { get; private set; }
		public Transform PursueTarget { get; private set; }
		public Transform MyAttacker { get; private set; }
		private float checkRate;
		private void SetInit()
		{
			checkRate = Random.Range(
				FSMSettings.checkRate - 0.15f, FSMSettings.checkRate + 0.15f);
		}
		
		private void OnEnable()
		{
			SetInit();
			
		}
		
		private void OnDisable()
		{
			
		}
	}
}
