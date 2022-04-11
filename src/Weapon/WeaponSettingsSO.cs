using UnityEngine;

namespace URPMk2
{
	[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WeaponSettings", order = 3)]
	public class WeaponSettingsSO : ScriptableObject
	{
		public int ammoCapacity;
		public float reloadTime;
		public float[] aimPosition;
		public string defaultAmmoCode;
		public string[] availableAmmoCodes;
		public GunSettings gunSettings;
		public BurstFireSettings burstFireSettings;
	}
	[System.Serializable]
	public class GunSettings
    {
		public int defaultFireMode;
		public int[] avaliableFireModes;
		public float shootRate; // shoots / min
    }
	[System.Serializable]
	public class BurstFireSettings
    {
		public int shootsInBurst;
		public float burstShootRate;
    }
}
