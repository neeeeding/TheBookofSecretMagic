using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapMark : MonoBehaviour
{
    private TMP_InputField memo;
    private bool isMemo;

    private void Awake()
    {
        memo = GetComponentInChildren<TMP_InputField>();
        isMemo = true;
        ClickMark();
    }

    public void ClickMark()
    {
        memo.gameObject.SetActive(!isMemo);
        isMemo = !isMemo;
    }

    public void InputMemo()
    {
        
    }
}
