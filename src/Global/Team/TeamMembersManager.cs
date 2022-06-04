using System.Collections.Generic;
using UnityEngine;

namespace URPMk2
{
	public static class TeamMembersManager
	{
		private static List<ITeamMember> allMembers;

		public static void RegisterInTeamMembers(ITeamMember t)
        {
			if (allMembers.Contains(t))
				return;

			allMembers.Add(t);
        }
		public static void RemoveFromTeamMembers(ITeamMember t)
        {
			if (allMembers.Contains(t))
				return;

			allMembers.Remove(t);
		}
	}
}
