using UnityEngine;

namespace URPMk2
{
	public class FinalDestination : MonoBehaviour
	{
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<IFinalDestinationEnjoyer>() == null)
                return;

            other.gameObject.GetComponent<FinalDestinationReceiver>().FinalDestinationReached();
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.GetComponent<IFinalDestinationEnjoyer>() == null)
                return;

            other.gameObject.GetComponent<IFinalDestinationEnjoyer>().FinalDestinationReached();
        }
    }
}
