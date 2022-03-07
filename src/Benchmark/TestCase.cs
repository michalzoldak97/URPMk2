using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace URPMk2
{
    public class TestCase : ITestable
    {
        private Vector2 vecZero = Vector2.zero;
        private Vector2 InitializeTestVec2()
        {
            Vector2 artificialInput = Vector2.zero;
            artificialInput.x = -2.5f;
            artificialInput.y = 2.5f;
            return artificialInput;
        }
        private void DoSomeJob()
        {
            Vector3 doSomeJob = new Vector3();
            doSomeJob.x = Mathf.Sqrt(123.3f - 431.5f);
            doSomeJob.y = Mathf.Sqrt(324.6f * 7.1f);
            doSomeJob.z = Mathf.Sqrt(435.1f / 23.5f);
        }
        private void PrimaryTestCase()
        {
            Vector2 artificialInput = InitializeTestVec2();
            if (artificialInput.x == 0.0f && artificialInput.y == 0.0f)
            {
                DoSomeJob();
            }
        }
        private void AlternativeTestCase()
        {
            Vector2 artificialInput = InitializeTestVec2();
            if (artificialInput != new Vector2(0, 0))
            {
                DoSomeJob();
            }
        }
        public void RunPrimaryTestCase()
        {
            PrimaryTestCase();
        }
        public void RunAlternativeTestCase()
        {
            AlternativeTestCase();
        }
    }
}
