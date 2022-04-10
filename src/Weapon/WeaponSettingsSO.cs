using UnityEngine;

namespace URPMk2
{
	[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WeaponSettings", order = 3)]
	public class WeaponSettingsSO : ScriptableObject
	{
		public float[] aimPosition;
		public GunSettings gunSettings;
		public BurstFireSettings burstFireSettings;
	}
	[System.Serializable]
	public class GunSettings
    {
		public bool autoModeAvailable;
		public bool burstModeAvaliable;
		public float shootRate; // shoots / min
    }
	[System.Serializable]
	public class BurstFireSettings
    {
		public int shootsInBurst;
		public float burstShootRate;
    }
}
