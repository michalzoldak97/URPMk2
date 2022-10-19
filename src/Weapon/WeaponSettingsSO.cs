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
		public GameObject projectileObj;
	}
	[System.Serializable]
	public class GunDamageSettings
    {
		public float funcCoeff;
		public float funcInter;
		public float penCoeff;
		public float penVar;
		public LayerMask layersToDamage;
    }
	[System.Serializable]
	public class GunSettings
    {
		public int defaultFireMode;
		public int sharpsNum;
		public int[] avaliableFireModes;
		public float shootRate; // shoots / min
		public float shootRange;
		public float recoil;
		public float[] shootStartPos;
		public LayerMask layersToHit;
		public string[] hitEffectTags; // 0: stone, 1: metal
	}
	[System.Serializable]
	public class BurstFireSettings
    {
		public int shootsInBurst;
		public float burstShootRate;
    }
}
