using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace URPMk2
{
	public static class GameVerifier
	{
        private const int startCargoNum = 15;
        private const string BASE_PTH = "D:/Documents/Studia/Praca/Results/game_summary_0_fsm_nbm.csv";

        private static int runID = 0;
        private static int cargoNum = 15;
        private static List<string> destroyedCargos = new List<string>();
		private static void FetchStatistics()
		{
            Dictionary<Teams, float> teamStat = new Dictionary<Teams, float>();

            foreach (DamageObjectData d in GlobalDamageMaster.dmgStatistics.Values)
            {
                if (teamStat.ContainsKey(d.objTeam))
                    teamStat[d.objTeam] += d.dmg;
                else
                    teamStat.Add(d.objTeam, d.dmg);
            }

            // string summaryHeader = "";
            string summaryVals = "\n";
            Teams[] dmgKeys = teamStat.Keys.ToArray();

            int dmgLen = dmgKeys.Length - 1;
            for (int i = 0; i <= dmgLen; i++)
            {
                // summaryHeader += string.Format("{0},", dmgKeys[i]);
                summaryVals += string.Format("{0},", teamStat[dmgKeys[i]]);
            }

            // summaryHeader += "Score";
            summaryVals += string.Format("{0}", GameScore.AttackersTeamScore);

            /*string summary = string.Format("{0}\n{1}", summaryHeader, summaryVals);
            string pth = BASE_PTH + runID.ToString() + ".csv";

            File.WriteAllText(pth, summary);*/
            File.AppendAllText(BASE_PTH, summaryVals);
        }
        private static void ResetScene()
        {
            runID++;
            cargoNum = startCargoNum;
            destroyedCargos.Clear();
            GlobalDamageMaster.Start();
            GameScore.Start();

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        public static void CargoDestroyed(Transform destroyedCargo)
        {
            string cargoID = destroyedCargo.name + destroyedCargo.GetInstanceID().ToString();

            if (destroyedCargos.Contains(cargoID))
                return;

            destroyedCargos.Add(cargoID);

            cargoNum--;

            Debug.Log("Cargo destroyed, cargo num is " + cargoNum);

            if (cargoNum < 1)
            {
                FetchStatistics();
                ResetScene();
            }
        }
	}
}
