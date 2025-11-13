using TMPro;
using UnityEngine;

namespace _02Script.UI.Map
{
    public class MapMarkMemo : MonoBehaviour
    {
        private TMP_InputField memo;
        private bool isMemo;
        private string savePath = "mapMemo_";

        private void Awake()
        {
            memo = GetComponentInChildren<TMP_InputField>();
            isMemo = true;
            ClickMark();
        }

        public void SetNum(int num)
        {
            savePath +=  num.ToString();
            memo.text = PlayerPrefs.GetString(savePath);
        }

        public void ClickMark()
        {
            isMemo = !isMemo;
            memo.gameObject.SetActive(isMemo);
        }

        public void InputMemo()
        {
            PlayerPrefs.SetString(savePath, memo.text);
            PlayerPrefs.Save();
        }
    }
}
