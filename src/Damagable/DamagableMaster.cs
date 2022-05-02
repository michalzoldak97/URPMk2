using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace URPMk2
{
    public class DamagableMaster : MonoBehaviour, IDamagableMaster
    {
        private void Start()
        {
            // register obj in dictionary   
        }
        public void CallEventHitByGun(float dmg, float pen)
        {

        }
        public void CallEventHitByExplosion(float dmg, float pen)
        {

        }
    }
}