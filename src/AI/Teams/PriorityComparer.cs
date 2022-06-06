using System.Collections.Generic;

namespace URPMk2
{
	public class PriorityComparer : IComparer<string>
	{
        public Dictionary<string, int> tagsPriorities;

        public PriorityComparer(Dictionary<string, int> tagsPriorities)
        {
            this.tagsPriorities = tagsPriorities;
        }
        public int Compare(string x, string y)
        {
            return tagsPriorities[x].CompareTo(tagsPriorities[y]);
        }
    }
}
