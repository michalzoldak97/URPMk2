using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace URPMk2
{
    public interface ITestable
    {
        public void RunPrimaryTestCase();
        public void RunAlternativeTestCase();
    }
}