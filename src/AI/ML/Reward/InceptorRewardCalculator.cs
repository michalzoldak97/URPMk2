using UnityEngine;

namespace URPMk2
{
	public class InceptorRewardCalculator
	{
		public InceptorRewardCalculator(Transform origin)
		{
			dmgKey = origin.name + origin.GetInstanceID();
			dmgMaster = origin.GetComponent<DamagableMaster>();
			// lastHealth = dmgMaster.GetHealth();
        }
		private float lastHealth;
		private float dmgInflicted;
		private readonly string dmgKey;
		private readonly DamagableMaster dmgMaster;

		private float[] GetData()
		{
            float[] inflictedReceived = new float[2];

			float currentDmg = GlobalDamageMaster.GetDamageForEntity(dmgKey);
            inflictedReceived[0] = currentDmg > dmgInflicted ? currentDmg - dmgInflicted : 0f;

			if (inflictedReceived[0] == 0f)
				inflictedReceived[1] += 10f; // penalty for not inflicting damage

            float currentHealth = dmgMaster.GetHealth();
            inflictedReceived[1] = currentHealth < lastHealth ? lastHealth - currentHealth : 0f;

            dmgInflicted = currentDmg;
            lastHealth = currentHealth;

            return inflictedReceived;
        }
		public float[] GetReward()
		{
			float[] inflictedReceived = GetData();

			inflictedReceived[0] *= 0.025f;
			inflictedReceived[1] *= -0.002f;

			return inflictedReceived;
        }
    }
}
