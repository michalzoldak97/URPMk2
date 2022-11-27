using TMPro;
using Unity.MLAgents;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Timeline;

namespace URPMk2
{
	public class GAILControlPanelManager : MonoBehaviour
	{
        [SerializeField] private GameObject agentPrefab;
        [SerializeField] private GameObject GAILPanel;
		[SerializeField] private Transform GAILCameraTransform;
        [SerializeField] private Transform marker;
        [SerializeField] private Transform[] agentSpawnPos;
        [SerializeField] private TMP_Text[] observations;

        private bool isGAILPanelActive, isClickOff;
        private Transform currentAgent;
        private Camera GAILCamera;
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
        private void SetGAILCameraPosition()
        {
            Vector3 aPos = currentAgent.position;
            aPos.y = GAILCameraTransform.transform.position.y;
            GAILCameraTransform.transform.position = aPos;
        }
        private void SpawnNewAgent()
        {
            Transform spawnPos = agentSpawnPos[Random.Range(0, agentSpawnPos.Length - 1)];
            GameObject agent = Instantiate(agentPrefab, spawnPos.position, spawnPos.rotation);
            currentAgent = agent.transform;
            igAgent = agent.GetComponent<InterceptorGAILAgent>();
            dmgMaster = agent.GetComponent<DamagableMaster>();
            igAgent.SetGAILManager(this);
            SetGAILCameraPosition();
        }
        private void StartNewEpisode(Transform killer)
        {
            SpawnNewAgent();
        }
        private void SetInit()
		{
            GAILCamera = GAILCameraTransform.GetComponent<Camera>();
            StartNewEpisode(transform);
        }
        private void OnEnable()
		{
            SetInit();
            InputManager.playerInputActions.Humanoid.ToggleGAILPanel.performed += EnableGAILPanelUI;
            InputManager.playerInputActions.Humanoid.ToggleGAILPanel.Enable();
            InputManager.playerInputActions.UI.ToggleGAILPanel.performed += DisableGAILPanelUI;
            InputManager.playerInputActions.UI.ToggleGAILPanel.Enable();
            InputManager.playerInputActions.UI.Click.performed += SetAgentDestination;
            dmgMaster.EventDestroyObject += StartNewEpisode;
        }
		
		private void OnDisable()
		{
            InputManager.playerInputActions.Humanoid.ToggleGAILPanel.performed -= EnableGAILPanelUI;
            InputManager.playerInputActions.Humanoid.ToggleGAILPanel.Disable();
            InputManager.playerInputActions.UI.ToggleGAILPanel.performed -= DisableGAILPanelUI;
            InputManager.playerInputActions.UI.ToggleGAILPanel.Disable();
            InputManager.playerInputActions.UI.Click.performed -= SetAgentDestination;
            dmgMaster.EventDestroyObject -= StartNewEpisode;
        }
        private void SetAgentDestination(InputAction.CallbackContext obj)
        {
            if (isClickOff)
            {
                isClickOff = !isClickOff;
                return;
            }

            Ray fromCamera = GAILCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(fromCamera, out RaycastHit hit))
            {
                Vector3 dirToPass = hit.point;
                dirToPass.y = 1f;
                Debug.Log("Dir to pass is: " + dirToPass);
                marker.position = dirToPass;
                igAgent.SetHDestination(dirToPass);
            }

            isClickOff = !isClickOff;
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
                CurrentMainCameraManager.SetCurrentCamera(GAILCameraTransform);
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
        private void FixedUpdate()
        {
            if (isGAILPanelActive &&
                currentAgent != null)
                SetGAILCameraPosition();
        }
    }
}
