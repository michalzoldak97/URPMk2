using UnityEngine;
using System.Collections;
using TMPro;

namespace URPMk2
{
    public class DmgText : MonoBehaviour, IPooledObject
    {
        [SerializeField] private TMP_Text myText;
        private Transform myTransform, cameraTransform;
        private Color32 textColor;
        private void SetInit()
        {
            myTransform = transform;
            cameraTransform = CurrentMainCameraManager.CurrentCamera;
        }
        private IEnumerator ShowDmgText()
        {
            textColor = DamagableDmgTextManager.startColor;
            Vector3 posToGo = myTransform.position;
            posToGo.y += 4;
            posToGo += Random.insideUnitSphere * 3;
            for (int i = 0; i < 60; i++)
            {
                yield return DamagableDmgTextManager.loopDelaySec;
                myTransform.LookAt(cameraTransform);
                myTransform.SetPositionAndRotation(Vector3.Lerp(myTransform.position, posToGo, 0.1f), 
                    Quaternion.LookRotation(cameraTransform.forward));
                if (textColor.a > 8)
                {
                    textColor.a -= 8;
                    myText.color = textColor;
                }
                else if(textColor.a > 0)
                {
                    textColor.a = 0;
                    myText.color = textColor; 
                    StopCoroutine(ShowDmgText());
                }
            }
        }
        private void OnEnable()
        {
            SetInit();
            StartCoroutine(ShowDmgText());
        }
        public void Activate() {}
        public void Activate(string toSet)
        {
            myText.text = toSet;
            StopAllCoroutines();
            SetInit();
            StartCoroutine(ShowDmgText());
        }
    }
}