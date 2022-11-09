using UnityEngine;
namespace URPMk2
{
    public class DamagableDmgText : MonoBehaviour
    {
        private Transform myTransform;
        private ObjectPool objectPool;
        private DamagableMaster dmgMaster;
        private void SetInit()
        {
            myTransform = transform;
            dmgMaster = GetComponent<DamagableMaster>();
        }
        private void Start()
        {
            try
            {
				objectPool = GameObject.FindGameObjectWithTag("SceneRoot").GetComponent<ObjectPool>();
			}
			catch
            {
				throw new MissingObjectException("Object Pooler");
			}
        }
        private void OnEnable()
        {
            SetInit();
            dmgMaster.EventHitByGun += ShowDmgText;
            dmgMaster.EventHitByExplosion += ShowDmgText;
        }
        private void OnDisable()
        {
            dmgMaster.EventHitByGun -= ShowDmgText;
            dmgMaster.EventHitByExplosion -= ShowDmgText;
        }
        private void ShowDmgText(DamageInfo dmgInfo)
        {
            PooledObjectInstance effInstance = objectPool.GetObjectFromPool(
                DamagableDmgTextManager.dmgTextTag);

            if (effInstance == null)
                return;

            effInstance.obj.transform.SetPositionAndRotation(myTransform.position, myTransform.rotation);
            effInstance.obj.SetActive(true);
			effInstance.objBehavior.Activate(dmgInfo.dmg.ToString("N1"));
        }
    }
}