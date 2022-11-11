using System;
using System.Collections.Generic;
using UnityEngine;

namespace URPMk2
{
	public static class TeamMembersManager
	{
		public static Dictionary<Teams, Vector3> TeamFinalDestinations = new Dictionary<Teams, Vector3>();
		private static bool isTeamMembersRefreshed;
		private static List<ITeamMember> allMembers = new List<ITeamMember>();
		private static List<ITeamMember> selectedMembers = new List<ITeamMember>();
		private static Dictionary<Teams, List<ITeamMember>> teamMembers = new Dictionary<Teams, List<ITeamMember>>();

		public static void RegisterInTeamMembers(ITeamMember t)
        {
			if (allMembers.Contains(t))
				return;

			allMembers.Add(t);
        }
		public static void RemoveFromTeamMembers(ITeamMember t)
        {
            if (!allMembers.Contains(t))
                return;

            allMembers.Remove(t);
		}
		private static void CreateTeamsMembers()
        {
			foreach (Teams teamID in Enum.GetValues(typeof(Teams)))
			{
				teamMembers.Add(teamID, new List<ITeamMember>());
			}
		}
		public static void Start()
        {
			CreateTeamsMembers();
		}
		async static void ResetIsTeamsMembersRefreshed()
        {
			await TimeSpan.FromSeconds(GameConfig.refreshTeamsEverySec);
			isTeamMembersRefreshed = false;
		}
		private static void RefreshTeamMembers()
        {
			foreach (Teams k in teamMembers.Keys)
            {
				teamMembers[k].Clear();
            }

			int allMembersLen = allMembers.Count;
			for (int i = 0; i < allMembersLen; i++)
			{
				teamMembers[allMembers[i].TeamID].Add(allMembers[i]);
			}

			isTeamMembersRefreshed = true;
			ResetIsTeamsMembersRefreshed();
		}
        private static void GetTeamMembersInRangeByTeam(Teams teamID, Vector3 pos, int rangePow)
        {
			if (!teamMembers.ContainsKey(teamID))
				return;

			ITeamMember[] membersInTeam = teamMembers[teamID].ToArray();
			int teamLen = membersInTeam.Length;
			for (int i = 0; i < teamLen; i++)
            {
				if ((membersInTeam[i].ObjTransform.position - pos).sqrMagnitude < rangePow)
					selectedMembers.Add(membersInTeam[i]);
			}
        }
        public static List<ITeamMember> GetTeamMembersInRange(Teams[] teams, Vector3 pos, int rangePow)
        {
			if (!isTeamMembersRefreshed)
				RefreshTeamMembers();

			selectedMembers.Clear();

			int teamsLen = teams.Length;
			for (int i = 0; i < teamsLen; i++)
            {
				GetTeamMembersInRangeByTeam(teams[i], pos, rangePow);
			}

			return selectedMembers;
		}
		public static List<ITeamMember> GetTeamMembersInRange(Teams team, Vector3 pos, int rangePow)
		{
			if (!isTeamMembersRefreshed)
				RefreshTeamMembers();

			selectedMembers.Clear();

			GetTeamMembersInRangeByTeam(team, pos, rangePow);

			return selectedMembers;
		}
		public static Vector3 GetFinalTeamDestination(Teams team)
        {
			if (!TeamFinalDestinations.ContainsKey(team))
				return Vector3.zero;

			if (TeamFinalDestinations[team] == null)
				return Vector3.zero;

			return TeamFinalDestinations[team];
		}
	}
}
