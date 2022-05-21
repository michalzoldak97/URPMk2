using UnityEngine;

namespace URPMk2
{
	public class DamagableEffects : MonoBehaviour
	{
		private GameObject destructionEffect;
		private DamagableMaster dmgMaster;
		private void SetInit()
		{
			dmgMaster = GetComponent<DamagableMaster>();
		}
		private void InitializeEffect()
        {
			GameObject[] destructionEffects = dmgMaster.GetDamagableSettings().destructionEffects;
			destructionEffect = Instantiate(
				destructionEffects[Random.Range(0, destructionEffects.Length)], 
				transform.position, 
				transform.rotation);
			destructionEffect.SetActive(false);
		}
        private void Start()
        {
			InitializeEffect();
		}
        private void OnEnable()
		{
			SetInit();
			dmgMaster.EventDestroyObject += SpawnDestructionEffect;
		}
		
		private void OnDisable()
		{
			dmgMaster.EventDestroyObject -= SpawnDestructionEffect;
		}
		private void SpawnDestructionEffect()
        {
			destructionEffect.transform.SetParent(null);
			destructionEffect.SetActive(true);
		}
	}
}
