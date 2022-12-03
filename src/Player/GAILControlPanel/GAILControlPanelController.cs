using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace URPMk2
{
	public class GAILControlPanelController : MonoBehaviour, IPointerClickHandler
	{
		[SerializeField] private Camera GAILPanelCamera;
		[SerializeField] private GAILControlPanelManager gcpManager;
		public Vector3 ScreenClickPoint { get; private set; }

		private RawImage screenImage;
		private void Awake()
		{
			screenImage = GetComponent<RawImage>();
        }
		
		private void CastRayToWorld(Vector2 cursorPos)
		{
			Ray worldRay = GAILPanelCamera.ScreenPointToRay(
				new Vector2(
					cursorPos.x * GAILPanelCamera.pixelWidth,
					cursorPos.y * GAILPanelCamera.pixelHeight
					));

			if (Physics.Raycast(worldRay, out RaycastHit hit, 1000f))
                ScreenClickPoint = hit.point;

			gcpManager.SetAgentDestination(ScreenClickPoint);
        }
		public void OnPointerClick(PointerEventData eventData)
		{
			Vector2 cursorPos = new Vector2(0f, 0f);

			if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
				screenImage.rectTransform,
				eventData.pressPosition,
				eventData.pressEventCamera,
				out cursorPos))
			{
				Texture texture = screenImage.texture;
				Rect rect = screenImage.rectTransform.rect;

				float coordX = Mathf.Clamp(0, (((cursorPos.x - rect.x) * texture.width) / rect.width), texture.width); 
				float coordY = Mathf.Clamp(0, (((cursorPos.y - rect.y) * texture.height) / rect.height), texture.height);

                float calX = coordX / texture.width;
                float calY = coordY / texture.height;

				cursorPos = new Vector2(calX, calY);

				CastRayToWorld(cursorPos);
            }
		}
	}
}
