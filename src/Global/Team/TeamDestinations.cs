using System.Collections.Generic;
using UnityEngine;

namespace URPMk2
{
	public class TeamDestinations : MonoBehaviour
	{
        [SerializeField] private TeamDestination[] finalTeamDestinationsList;
        private Dictionary<Teams, Vector3> finalTeamDestinations;

        private void Start()
        {
            finalTeamDestinations = new Dictionary<Teams, Vector3>();
            foreach (TeamDestination td in finalTeamDestinationsList)
            {
                finalTeamDestinations.Add(td.team, td.destination.position);
            }
            TeamMembersManager.TeamFinalDestinations = finalTeamDestinations;
        }
    }
    [System.Serializable]
    public struct TeamDestination
    {
        public Teams team;
        public Transform destination;
    }
}
