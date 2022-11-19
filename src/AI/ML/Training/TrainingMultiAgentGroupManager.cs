using Unity.MLAgents;
using UnityEngine;

namespace URPMk2
{
    public class TrainingMultiAgentGroupManager : MonoBehaviour
    {
        [SerializeField] private int groupID;
        [SerializeField] private Transform[] agentSpawnPositions;
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
            tuManager.EventStartNewEpisode += StartGroupEpisode;
            tuManager.EventEndEpisode += EndGroupEpisode;
            tuManager.EventAddGroupReward += GetGroupReward;
        }

        private void OnDisable()
        {
            tuManager.EventStartNewEpisode -= StartGroupEpisode;
            tuManager.EventEndEpisode -= EndGroupEpisode;
            tuManager.EventAddGroupReward -= GetGroupReward;
        }
        private void InstantiateNewAgentGroup()
        {
            foreach(Transform sPos in agentSpawnPositions)
            {
                GameObject aObj = Instantiate(agentPrefab, sPos.position, sPos.rotation);
                aObj.GetComponent<TrainingUnitInstance>().SetTrainingUnitManager(tuManager);
                IMultiAgentGroupMember agent = new MultiAgentGroupMember(groupID, aObj.transform, aObj.GetComponent<Agent>());
                tuManager.RegisterAgent(agent);
                MultiAgentGroup.RegisterAgent(agent.Agent);
            }
        }
        private void StartGroupEpisode()
        {
            InstantiateNewAgentGroup();
        }
        private void GetGroupReward(int groupID, float reward)
        {
            if (groupID != this.groupID)
                return;

            MultiAgentGroup.AddGroupReward(reward);
            Debug.Log("Group of id " + groupID + " recieves reward ");
        }
        private void EndGroupEpisode(int foo, int barr)
        {
            MultiAgentGroup.EndGroupEpisode();
        }
    }
}