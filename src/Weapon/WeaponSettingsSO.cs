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
		public AudioClip shootSound;
		public AudioClip reloadSound;
		public GunSettings gunSettings;
		public GunDamageSettings damageSettings;
		public BurstFireSettings burstFireSettings;
	}
	[System.Serializable]
	public class GunDamageSettings
    {
		public float funcCoeff;
		public float funcInter;
		public float penCoeff;
		public float penVar;
		public LayerMask layersToHit;
		public LayerMask layersToDamage;
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
