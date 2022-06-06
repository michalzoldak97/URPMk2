using System;
using System.Collections.Generic;
using UnityEngine;

namespace URPMk2
{
    public class TestCompareTM
    {
        public ITeamMember teamMember;
        public float dist = -5;

        public TestCompareTM(ITeamMember t, float d)
        {
            teamMember = t;
            dist = d;
        }
    }
    public class TMComparer : IComparer<TestCompareTM>
    {
        public int Compare(TestCompareTM a, TestCompareTM b)
        {
            return a.dist.CompareTo(b.dist);
        }
    }
    public class TestCase_ToArray : MonoBehaviour, ITestable
    {
        private List<ITeamMember> allMembers = new List<ITeamMember>();
        private List<ITeamMember> selectedMembers = new List<ITeamMember>();
        private Dictionary<Teams, List<ITeamMember>> teamMembers = new Dictionary<Teams, List<ITeamMember>>();

        private void InitData()
        {
            for (int i = 0; i < 400; i++)
            {
                GameObject tObj = new GameObject(i.ToString());
                tObj.AddComponent<CapsuleCollider>();
                tObj.AddComponent<TeamMember>();
                tObj.GetComponent<TeamMember>().TeamID = (Teams)(UnityEngine.Random.Range(0, 4));
                allMembers.Add(tObj.GetComponent<TeamMember>());
            }
        }
        private void CreateTeamsMembers()
        {
            foreach (Teams teamID in Enum.GetValues(typeof(Teams)))
            {
                teamMembers.Add(teamID, new List<ITeamMember>());
            }
        }
        private void RefreshTeamMembers()
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
        }
        private void Start()
        {
            InitData();
            CreateTeamsMembers();
            RefreshTeamMembers();
        }
        private void GetTeamMembersInRangeByTeam(Teams teamID, Vector3 pos, int rangePow)
        {
            if (!teamMembers.ContainsKey(teamID))
                return;

            ITeamMember[] membersInTeam = teamMembers[teamID].ToArray();
            int teamLen = membersInTeam.Length;
            for (int i = 0; i < teamLen; i++)
            {
                float d = (membersInTeam[i].GetPos() - pos).sqrMagnitude;
                if (d < rangePow)
                {
                    membersInTeam[i].DistTo = d;
                    selectedMembers.Add(membersInTeam[i]);
                }
            }
        }
        public List<ITeamMember> GetTeamMembersInRange(Teams[] teams, Vector3 pos, int rangePow)
        {
            selectedMembers.Clear();

            int teamsLen = teams.Length;
            for (int i = 0; i < teamsLen; i++)
            {
                GetTeamMembersInRangeByTeam(teams[i], pos, rangePow);
            }

            selectedMembers.Sort((x, y) => x.DistTo.CompareTo(y.DistTo));
            return selectedMembers;
        }

        private void GetTeamMembersInRangeByTeamStruct(Teams teamID, Vector3 pos, int rangePow)
        {
            if (!teamMembers.ContainsKey(teamID))
                return;

            ITeamMember[] membersInTeam = teamMembers[teamID].ToArray();
            int teamLen = membersInTeam.Length;
            for (int i = 0; i < teamLen; i++)
            {
                float d = (membersInTeam[i].GetPos() - pos).sqrMagnitude;
                if (d < rangePow)
                {
                    membersInTeam[i].DistTo = d;
                    selectedMembers.Add(membersInTeam[i]);
                }
            }
        }
        public List<ITeamMember> GetTeamMembersInRangeStruct(Teams[] teams, Vector3 pos, int rangePow)
        {
            selectedMembers.Clear();

            int teamsLen = teams.Length;
            for (int i = 0; i < teamsLen; i++)
            {
                GetTeamMembersInRangeByTeamStruct(teams[i], pos, rangePow);
            }

            return selectedMembers;
        }
        public void RunAlternativeTestCase()
        {
            Teams[] teams = new Teams[] { Teams.Enemy, Teams.Player, Teams.LightFriendly, Teams.MediumFriendly, Teams.HeavyFriendly };
            Vector3 pos = new Vector3(5, 5, 5);
            List<ITeamMember> tList = GetTeamMembersInRangeStruct(teams, pos, 999);
        }

        public void RunPrimaryTestCase()
        {
            Teams[] teams = new Teams[] { Teams.Enemy, Teams.Player, Teams.LightFriendly, Teams.MediumFriendly, Teams.HeavyFriendly };
            Vector3 pos = new Vector3(5, 5, 5);
            List<ITeamMember> tList = GetTeamMembersInRange(teams, pos, 999);
        }
    }
}
