using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace URPMk2
{
	public class GAILControlPanelManager : MonoBehaviour
	{
        [SerializeField] private GameObject agentPrefab;
        [SerializeField] private GameObject GAILPanel;
		[SerializeField] private Transform GAILCamera;
        [SerializeField] private Transform agentSpawnPos;
        [SerializeField] private TMP_Text[] observations;

        private bool isGAILPanelActive;
        private InterceptorGAILAgent igAgent;
        private DamagableMaster dmgMaster;

        public void UpdateObservations(Vector3 agentPos, Vector3 enemyPos, Vector3 spottedPos, float health)
        {
            observations[0].text = agentPos.x.ToString();
            observations[1].text = agentPos.y.ToString();
            observations[2].text = agentPos.z.ToString();
            observations[3].text = enemyPos.x.ToString();
            observations[4].text = enemyPos.y.ToString();
            observations[5].text = enemyPos.z.ToString();
            observations[6].text = spottedPos.x.ToString();
            observations[7].text = spottedPos.y.ToString();
            observations[8].text = spottedPos.z.ToString();
            observations[9].text = health.ToString();
        }
        private void StartNewEpisode()
        {
            GameObject agent = Instantiate(agentPrefab, agentSpawnPos.position, agentSpawnPos.rotation);
            igAgent = agent.GetComponent<InterceptorGAILAgent>();
            dmgMaster = agent.GetComponent<DamagableMaster>();
            igAgent.SetGAILManager(this);
            Vector3 aPos = agent.transform.position;
            aPos.y = GAILCamera.transform.position.y;
            GAILCamera.transform.position = aPos;
        }
        private void SetInit()
		{
            StartNewEpisode();
        }
        private void OnEnable()
		{
            SetInit();
            InputManager.playerInputActions.Humanoid.ToggleGAILPanel.performed += EnableGAILPanelUI;
            InputManager.playerInputActions.Humanoid.ToggleGAILPanel.Enable();
            InputManager.playerInputActions.UI.ToggleGAILPanel.performed += DisableGAILPanelUI;
            InputManager.playerInputActions.UI.ToggleGAILPanel.Enable();
        }
		
		private void OnDisable()
		{
            InputManager.playerInputActions.Humanoid.ToggleGAILPanel.performed -= EnableGAILPanelUI;
            InputManager.playerInputActions.Humanoid.ToggleGAILPanel.Disable();
            InputManager.playerInputActions.UI.ToggleGAILPanel.performed -= DisableGAILPanelUI;
            InputManager.playerInputActions.UI.ToggleGAILPanel.Disable();
        }

		private void ToggleGAILPanelUI()
		{
            CursorManager.ToggleCursorState(isGAILPanelActive);
            if (isGAILPanelActive)
                InputManager.ToggleActionMap(InputManager.playerInputActions.UI);
            else
                InputManager.ToggleActionMap(InputManager.playerInputActions.Humanoid);
            if (isGAILPanelActive)
            {
                InputManager.playerInputActions.UI.Enable();
                InputManager.playerInputActions.Humanoid.Disable();
            }
            else
            {
                InputManager.playerInputActions.UI.Disable();
                InputManager.playerInputActions.Humanoid.Enable();
            }

            GAILCamera.gameObject.SetActive(isGAILPanelActive);

            if (isGAILPanelActive)
                CurrentMainCameraManager.SetCurrentCamera(GAILCamera);
            else
                CurrentMainCameraManager.RestoreMainCamera();

            GAILPanel.SetActive(isGAILPanelActive);
        }
        private void EnableGAILPanelUI(InputAction.CallbackContext obj)
        {
            isGAILPanelActive = !isGAILPanelActive;
            ToggleGAILPanelUI();
        }
        private void DisableGAILPanelUI(InputAction.CallbackContext obj)
        {
            isGAILPanelActive = !isGAILPanelActive;
            ToggleGAILPanelUI();
        }
    }
}
