using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;
using System.IO;
using System.Reflection;
using System;

public class LikeabilityCard : MonoBehaviour
{
    [SerializeField] private CharacterName characterName; //대상 이름

    [SerializeField] private TextMeshProUGUI valueText; //호감도 표시 텍스트
    [SerializeField] private Slider valueSlider; //호감도 슬라이더
    [SerializeField]private TMP_InputField memo; //메모
    private int loveValue; //호감도 (수)

    private void Start()
    {
        memo.text = PlayerPrefs.GetString($"{characterName}Memo");
        GameManager.OnStart += GameStart;
        LoadCard.OnLoad += LoadData;
    }

    private void GameStart()
    {
        SaveMyLoveValue(false);
    }

    public void Click()
    {
        UISettingManager.Instance.LiKeItme();
    }

    public void InputText()
    {
        string value = memo.text;
        PlayerPrefs.SetString($"{characterName}Memo", value);
        PlayerPrefs.Save();
    }

    [ContextMenu("LoveLove")]
    private void LoveLove()
    {
        LoveUp(10);
    }
    private void LoveUp(int value)
    {
        loveValue += value;
        valueText.text = $"{loveValue} / 100 ";
        valueSlider.value = loveValue;
        PlayerPrefs.SetInt($"{characterName}Love", loveValue);
        PlayerPrefs.Save();
        SaveMyLoveValue(true);
    }

    private void LoadData()
    {
        SaveMyLoveValue(false);
    }

    private void SaveMyLoveValue(bool set)
    {
        FieldInfo field = GameManager.Instance.PlayerStat.GetType().GetField(characterName.ToString(), BindingFlags.Public | BindingFlags.Instance);
        if(field != null && field.FieldType == typeof(int))
        {
            if(set)
            {
                field.SetValue(GameManager.Instance.PlayerStat, loveValue); //플레이어에게 저장해주기
            }
            else
            {
                PlayerPrefs.SetInt($"{characterName}Love", ((int)field.GetValue(GameManager.Instance.PlayerStat))); //값 저장하기. (실상은 불러오기)
                PlayerPrefs.Save();
                loveValue = PlayerPrefs.GetInt($"{characterName}Love");
                LoveUp(0);
            }
        }
    }

    private void OnDisable()
    {
        GameManager.OnStart -= GameStart;
        LoadCard.OnLoad += LoadData;
    }
}
