using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
        [SerializeField] private Image[] directionImages;
        [SerializeField] private GAILControlPanelController panelController;

        private bool isGAILPanelActive;
        private Transform currentAgent;
        private Camera GAILCamera;
        private InterceptorGAILAgent igAgent;
        private DamagableMaster dmgMaster;

        public delegate void AgentDirectionSetManager();

        private void UpdateDirectionImage(Vector3 agentPos, Vector3 enemyPos)
        {
            Vector3 pos = new Vector3(-1f, -1f, -1f);
            if (enemyPos == pos)
            {
                foreach (Image img in directionImages)
                {
                    img.color = Color.white;
                }

                return;
            }

            if (agentPos.x > enemyPos.x)
            {
                directionImages[2].color = Color.red;
                directionImages[6].color = Color.white;
            }
            else
            {
                directionImages[2].color = Color.white;
                directionImages[6].color = Color.red;
            }
            if (agentPos.z > enemyPos.z)
            {
                directionImages[0].color = Color.red;
                directionImages[4].color = Color.white;
            }
            else
            {
                directionImages[0].color = Color.white;
                directionImages[4].color = Color.red;
            }
        }
        public void UpdateObservations(Vector3 agentPos, Vector3 enemyPos, Vector3 spottedPos, float health, float reward)
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
            observations[10].text = reward.ToString();

            UpdateDirectionImage(agentPos, enemyPos);
        }
        private void SetGAILCameraPosition()
        {
            Vector3 aPos = currentAgent.position;
            aPos.y = GAILCameraTransform.transform.position.y;
            aPos.z -= 20f;
            GAILCameraTransform.transform.position = aPos;
        }
        private void SpawnNewAgent()
        {
            Transform spawnPos = agentSpawnPos[Random.Range(0, agentSpawnPos.Length - 1)];
            GameObject agent = Instantiate(agentPrefab, spawnPos.position, spawnPos.rotation);
            currentAgent = agent.transform;
            igAgent = agent.GetComponent<InterceptorGAILAgent>();
            dmgMaster = agent.GetComponent<DamagableMaster>();
            dmgMaster.EventDestroyObject += StartNewEpisode;
            igAgent.SetGAILManager(this);
            SetGAILCameraPosition();
        }
        public void OnEpisodeWon()
        {
            igAgent.OnAgentWon();
            currentAgent.gameObject.SetActive(false);
            Destroy(currentAgent.gameObject, GameConfig.secToDestroy);
            SpawnNewAgent();
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
        }
		
		private void OnDisable()
		{
            InputManager.playerInputActions.Humanoid.ToggleGAILPanel.performed -= EnableGAILPanelUI;
            InputManager.playerInputActions.Humanoid.ToggleGAILPanel.Disable();
            InputManager.playerInputActions.UI.ToggleGAILPanel.performed -= DisableGAILPanelUI;
            InputManager.playerInputActions.UI.ToggleGAILPanel.Disable();
        }
        public void SetAgentDestination(Vector3 toSetPos)
        {
            igAgent.SetHDestination(toSetPos);
            marker.position = toSetPos;
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
