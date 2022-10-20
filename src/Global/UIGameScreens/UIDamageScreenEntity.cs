using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace URPMk2
{
	public class UIDamageScreenEntity : MonoBehaviour
	{
        [SerializeField] private TMP_Text tName;
        [SerializeField] private TMP_Text tDmg;
        public void SetUIDamageScreenEntity(float dmg, string origin)
        {
            tDmg.text = dmg.ToString();
            tName.text = origin;
        }
    }
}
