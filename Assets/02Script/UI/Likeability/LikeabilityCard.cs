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
    [SerializeField] private CharacterName characterName; //��� �̸�

    [SerializeField] private TextMeshProUGUI valueText; //ȣ���� ǥ�� �ؽ�Ʈ
    [SerializeField] private Slider valueSlider; //ȣ���� �����̴�
    [SerializeField]private TMP_InputField memo; //�޸�
    private int loveValue; //ȣ���� (��)

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
                field.SetValue(GameManager.Instance.PlayerStat, loveValue); //�÷��̾�� �������ֱ�
            }
            else
            {
                PlayerPrefs.SetInt($"{characterName}Love", ((int)field.GetValue(GameManager.Instance.PlayerStat))); //�� �����ϱ�. (�ǻ��� �ҷ�����)
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
