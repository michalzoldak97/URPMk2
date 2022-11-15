using Unity.MLAgents;
using UnityEngine;

namespace URPMk2
{
	public class InceptorRewardCalculator
	{
		public InceptorRewardCalculator(Agent myAgent, Transform origin)
		{
			this.myAgent = myAgent;
            dmgKey = origin.name + origin.GetInstanceID();
			dmgMaster = origin.GetComponent<DamagableMaster>();
            GlobalDamageMaster.EventRegisterDestruction += VerifyFrag;
        }
		private float lastHealth;
		private float dmgInflicted;
		private readonly string dmgKey;
		private readonly Transform agentTransform;
		private readonly DamagableMaster dmgMaster;
		private readonly Agent myAgent;

		private void VerifyFrag(Transform origin)
		{
			if (origin == agentTransform)
				myAgent.AddReward(0.25f);
		}

        private float[] GetData()
		{
            float[] inflictedReceived = new float[2];

			float currentDmg = GlobalDamageMaster.GetDamageForEntity(dmgKey);
            inflictedReceived[0] = currentDmg > dmgInflicted ? currentDmg - dmgInflicted : 0f;

			if (inflictedReceived[0] == 0f)
				inflictedReceived[1] += 1f; // penalty for not inflicting damage

            float currentHealth = dmgMaster.GetHealth();
            inflictedReceived[1] = currentHealth < lastHealth ? lastHealth - currentHealth : 0f;

            dmgInflicted = currentDmg;
            lastHealth = currentHealth;

            return inflictedReceived;
        }
		public float[] GetReward()
		{
			float[] inflictedReceived = GetData();

			inflictedReceived[0] *= 0.02f;
			inflictedReceived[1] *= -0.002f;

			return inflictedReceived;
        }
    }
}
