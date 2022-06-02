using UnityEngine;
using System.Collections.Generic;
using System;

namespace URPMk2
{
    public class TestTeamObject
    {
        public int teamID;
        public TestTeamObject(int teamID)
        {
            this.teamID = teamID;
        }
    }
	public class TestCase_ChackInArray : MonoBehaviour, ITestable
	{
        private int[] myTeams = new int[] { 1, 3, 5 };
        private List<TestTeamObject> testObjects;
        private void Start()
        {
            testObjects = new List<TestTeamObject>();
            for (int i = 0; i < 100; i++)
            {
                testObjects.Add(new TestTeamObject(UnityEngine.Random.Range(0, 20)));
            }
        }
        private void DoSth()
        {
            Vector3 dummy = new Vector3(0.5f, 0.5f, 0.5f);
            dummy.x = dummy.z * dummy.y;
        }
        public void RunPrimaryTestCase()
        {
            for (int i = 0; i < testObjects.Count; i++)
            {
                if (Array.Exists(myTeams, el => el == testObjects[i].teamID))
                    DoSth();
            }
        }
        private bool Contains(Array a, int el)
        {
            return Array.IndexOf(a, el) != -1;
        }
        public void RunAlternativeTestCase()
        {
            for (int i = 0; i < testObjects.Count; i++)
            {
                if (Contains(myTeams, testObjects[i].teamID))
                    DoSth();
            }
        }
    }
}
