using UnityEngine;

namespace URPMk2
{
	public class ExplosiveMissileImpulse : MonoBehaviour
	{
        [SerializeField] private float impulseForce;
        private void OnEnable()
        {
            gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * impulseForce, ForceMode.Impulse);
        }
    }
}
