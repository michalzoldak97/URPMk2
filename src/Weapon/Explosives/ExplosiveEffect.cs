using System.Collections;
using UnityEngine;

namespace URPMk2
{
	public class ExplosiveEffect : MonoBehaviour
	{
        private GameObject destructionEffect;
        private ExplosiveMaster explosiveMaster;
        private void SetInit()
        {
            explosiveMaster = GetComponent<ExplosiveMaster>();
        }
        private void InitializeEffect()
        {
            destructionEffect = Instantiate(
                explosiveMaster.GetExplosiveSettings().explosionEffect,
                transform.position,
                transform.rotation,
                transform);
            destructionEffect.SetActive(false);
        }
        private void Start()
        {
            InitializeEffect();
        }
        private void OnEnable()
        {
            SetInit();
            explosiveMaster.EventExplode += SpawnDestructionEffect;
        }

        private void OnDisable()
        {
            explosiveMaster.EventExplode -= SpawnDestructionEffect;
        }
        private IEnumerator Deactivate()
        {
            Destroy(destructionEffect, GameConfig.secToDestroy);
            yield return GameConfig.waitEffectAlive;
            gameObject.SetActive(false);
        }
        private void SpawnDestructionEffect()
        {
            destructionEffect.transform.SetParent(null);
            destructionEffect.transform.position += explosiveMaster.GetExplosiveSettings().effectOffset;
            destructionEffect.SetActive(true);
            StartCoroutine(Deactivate());
        }
    }
}
