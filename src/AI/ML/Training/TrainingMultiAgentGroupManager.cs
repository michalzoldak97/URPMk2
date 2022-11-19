using Unity.MLAgents;
using UnityEngine;

namespace URPMk2
{
    public class TrainingMultiAgentGroupManager : MonoBehaviour
    {
        [SerializeField] private int groupID;
        [SerializeField] private int numAgents;
        [SerializeField] private GameObject agentPrefab;
        public SimpleMultiAgentGroup MultiAgentGroup { get; private set; }
        private TrainingUnitManager tuManager;

        private void SetInit()
        {
            MultiAgentGroup = new SimpleMultiAgentGroup();
            tuManager = GetComponent<TrainingUnitManager>();
        }

        private void OnEnable()
        {
            SetInit();

        }

        private void OnDisable()
        {

        }

    }
}