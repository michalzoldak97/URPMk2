using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Linq;

namespace URPMk2
{
	public class UIDamageStatisticsScreen : MonoBehaviour
	{
        [SerializeField] private TMP_Text summaryText;
        [SerializeField] private GameObject dmgScreenParent;
        [SerializeField] private GameObject dmgScreenEntityPrefab;
        [SerializeField] private Transform dmgScreenContent;
        private bool isDmgScreenActive;
		private void OnEnable()
		{
            InputManager.playerInputActions.Humanoid.ToggleDamageSettings.performed += EnableDamageStatisticsScreen;
            InputManager.playerInputActions.Humanoid.ToggleDamageSettings.Enable();
            InputManager.playerInputActions.UI.ToggleDamageScreen.performed += DisableDamageStatisticsScreen;
            InputManager.playerInputActions.UI.ToggleDamageScreen.Enable();
        }
		private void OnDisable()
		{
            InputManager.playerInputActions.Humanoid.ToggleDamageSettings.performed -= EnableDamageStatisticsScreen;
            InputManager.playerInputActions.Humanoid.ToggleDamageSettings.Disable();
            InputManager.playerInputActions.UI.ToggleDamageScreen.performed -= DisableDamageStatisticsScreen;
            InputManager.playerInputActions.UI.ToggleDamageScreen.Disable();
        }
        private void ToggleDmgStatUI()
        {
            CursorManager.ToggleCursorState(isDmgScreenActive);
            if (isDmgScreenActive)
                InputManager.ToggleActionMap(InputManager.playerInputActions.UI);
            else
                InputManager.ToggleActionMap(InputManager.playerInputActions.Humanoid);
            if (isDmgScreenActive)
            {
                InputManager.playerInputActions.UI.Enable();
                InputManager.playerInputActions.Humanoid.Disable();
            }
            else
            {
                InputManager.playerInputActions.UI.Disable();
                InputManager.playerInputActions.Humanoid.Enable();
            }
            dmgScreenParent.SetActive(isDmgScreenActive);
        }
        private void EnableDamageStatisticsScreen(InputAction.CallbackContext obj)
		{
            isDmgScreenActive = !isDmgScreenActive;
            ToggleDmgStatUI();
            BuildDmgScreen();
        }
        private void DisableDamageStatisticsScreen(InputAction.CallbackContext obj)
        {
            isDmgScreenActive = !isDmgScreenActive;
            ToggleDmgStatUI();
        }
        private void ClearUI()
        {
            foreach (Transform UIElement in dmgScreenContent)
            {
                Destroy(UIElement.gameObject);
            }
        }
        private void AddDamageInfoEntity(float dmg, string origin)
        {
            GameObject d = Instantiate(dmgScreenEntityPrefab, dmgScreenContent);
            d.GetComponent<UIDamageScreenEntity>().SetUIDamageScreenEntity(dmg, origin);
        }
        private void UpdateSummaryText()
        {
            summaryText.text = "";
            Dictionary<Teams, float> teamStat = new Dictionary<Teams, float>();
            foreach (DamageObjectData d in GlobalDamageMaster.dmgStatistics.Values)
            {
                if (teamStat.ContainsKey(d.objTeam))
                    teamStat[d.objTeam] += d.dmg;
                else
                    teamStat.Add(d.objTeam, d.dmg);
            }
            foreach (KeyValuePair<Teams, float> tf in teamStat)
            {
                summaryText.text += "Team " + tf.Key.ToString() + " Damage: " + tf.Value.ToString() + "\n";
            }

            summaryText.text += "Attacker Team Score: " + GameScore.AttackersTeamScore + "\n";
        }
        public void BuildDmgScreen()
        {
            ClearUI();
            UpdateSummaryText();
            foreach (KeyValuePair<string, DamageObjectData> td in GlobalDamageMaster.dmgStatistics.OrderByDescending( x => x.Value.dmg))
            {
                AddDamageInfoEntity(td.Value.dmg, td.Key);
            }
        }
	}
}
