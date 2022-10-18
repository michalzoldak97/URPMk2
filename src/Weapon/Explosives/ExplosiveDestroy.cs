using System.Collections;
using UnityEngine;

namespace URPMk2
{
    public class ExplosiveDestroy : MonoBehaviour
    {
        private ExplosiveMaster explosiveMaster;

        private void SetInit()
        {
            explosiveMaster = GetComponent<ExplosiveMaster>();
        }
        private void OnEnable()
        {
            SetInit();
            explosiveMaster.EventExplode += StartDisableObj;
        }
        private void OnDisable()
        {
            explosiveMaster.EventExplode -= StartDisableObj;
        }
        private IEnumerator DisableObj()
        {
            yield return new WaitForSeconds(explosiveMaster.GetExplosiveSettings().expDisableAfterSec);
            Destroy(gameObject, GameConfig.secToDestroy);
            gameObject.SetActive(false);
        }
        private void StartDisableObj()
        {
            StartCoroutine(DisableObj());
        }
    }
}