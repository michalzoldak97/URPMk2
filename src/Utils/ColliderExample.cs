using UnityEngine;

namespace URPMk2
{
	public class ColliderExample : MonoBehaviour
	{
        private float nextCheck;
        private float checkRate = 1f;
        private Collider myCollider;
        private void Start()
        {
            myCollider = gameObject.GetComponent<Collider>();
        }
        private void Update()
        {
            if(Time.time > nextCheck)
            {
                nextCheck = Time.time + checkRate;
                Debug.Log("Collider extents are: " + myCollider.bounds + "\n pos is: " + transform.position);
            }
        }
    }
}
