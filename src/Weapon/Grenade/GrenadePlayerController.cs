using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace URPMk2
{
	public class GrenadePlayerController : MonoBehaviour, IActionMapChangeSensitive
    {
        [SerializeField] int maxForce;
        [SerializeField] int increaseRate;
        [SerializeField] GameObject grenade;
        [SerializeField] GameObject cotter;
        [SerializeField] Slider progressBar;

        private bool isIncreasingForce;
        private float currForce;
        private float nextCheck, checkRate = 0.1f;
        private ItemMaster itemMaster;
        private WeaponMaster weaponMaster;
        private GameObject canvas;

        private void SetInit()
		{
            itemMaster = GetComponent<ItemMaster>();
            weaponMaster = GetComponent<WeaponMaster>();
            canvas = progressBar.transform.parent.gameObject;

            if (itemMaster.isSelectedOnParent)
                canvas.SetActive(true);
        }
		
		private void OnEnable()
		{
			SetInit();
            itemMaster.EventActivateOnParent += ActivateSliderCanvas;
            itemMaster.EventItemThrow += OnThrow;
            weaponMaster.EventAimRequest += HandleThrowForce;
            weaponMaster.EventPullTrigger += ThrowGrenade;
            InputManager.actionMapChange += InputMapChange;
        }
		
		private void OnDisable()
		{
            canvas.SetActive(false);
            itemMaster.EventActivateOnParent -= ActivateSliderCanvas;
            itemMaster.EventItemThrow -= OnThrow;
            weaponMaster.EventAimRequest -= HandleThrowForce;
            weaponMaster.EventPullTrigger -= ThrowGrenade;
            InputManager.actionMapChange -= InputMapChange;
        }
        private void ActivateSliderCanvas()
        {
            canvas.SetActive(true);
        }
        private void OnThrow(Transform dummy)
        {
            isIncreasingForce = false;
            currForce = 0;
            progressBar.value = 0;
            cotter.SetActive(true);
            canvas.SetActive(false);
        }
        private void HandleThrowForce()
        {
            if (!itemMaster.isSelectedOnParent)
                return;

            isIncreasingForce = !isIncreasingForce;

            if (!isIncreasingForce)
            {
                currForce = 0;
                progressBar.value = 0;
                cotter.SetActive(true);
                return;
            }

            cotter.SetActive(false);
        }
        private IEnumerator RemoveObject()
        {
            transform.root.GetComponent<PlayerInventoryMaster>().CallEventItemThrowRequested(transform);
            yield return new WaitForEndOfFrame();
            Destroy(gameObject);
        }
        private void ThrowGrenade()
        {
            if (!itemMaster.isSelectedOnParent ||
                weaponMaster.isShootState)
                return;

            weaponMaster.SetIsShootState(true);
            GameObject go = Instantiate(grenade, transform.position, transform.rotation);
            go.GetComponent<Rigidbody>().AddForce(transform.parent.transform.forward * currForce, ForceMode.Impulse);
            StartCoroutine(RemoveObject());
        }

        private void Start()
        {
            canvas.SetActive(false);

            progressBar.value = 0;
            progressBar.maxValue = maxForce;
        }

        private void IncreaseTrhowForce()
        {
            if (!itemMaster.isSelectedOnParent ||
                !isIncreasingForce ||
                currForce >= maxForce)
                return;

            currForce += (float)maxForce / increaseRate;
            progressBar.value = currForce;
        }

        private void Update()
        {

            if (Time.time < nextCheck)
                return;

            nextCheck = Time.time + checkRate;

            IncreaseTrhowForce();
        }

        public void InputMapChange(InputActionMap actionMapToSet)
        {
            isIncreasingForce = false;
            currForce = 0;
            cotter.SetActive(true);
            progressBar.value = 0;
        }
    }
}
