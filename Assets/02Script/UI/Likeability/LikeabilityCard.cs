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
    [SerializeField] private CharacterSO character; //��� ����

    [SerializeField] private Image characterImage;
    [SerializeField] private TextMeshProUGUI valueText; //ȣ���� ǥ�� �ؽ�Ʈ
    [SerializeField] private Slider valueSlider; //ȣ���� �����̴�
    [SerializeField]private TMP_InputField memo; //�޸�
    private int loveValue; //ȣ���� (��)

    private void Awake()
    {
        //characterImage.sprite = character.characterImage;
        memo.text = PlayerPrefs.GetString($"{character.characterName}Memo");
        GameManager.OnStart += LoadData;
    }

    private void OnEnable()
    {
        LoadCard.OnLoad += LoadData;
    }

    public void Click()
    {
        UISettingManager.Instance.LiKeItme();
    }

    public void InputText()
    {
        string value = memo.text;
        PlayerPrefs.SetString($"{character.characterName}Memo", value);
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
        PlayerPrefs.SetInt($"{character.characterName}Love", loveValue);
        PlayerPrefs.Save();
        SaveMyLoveValue(true);
    }

    private void LoadData()
    {
        print("d");
        SaveMyLoveValue(false);
    }

    private void SaveMyLoveValue(bool set)
    {
        FieldInfo field = GameManager.Instance.PlayerStat.GetType().GetField(character.characterName.ToString(), BindingFlags.Public | BindingFlags.Instance);
        if(field != null && field.FieldType == typeof(int))
        {
            if(set)
            {
                field.SetValue(GameManager.Instance.PlayerStat, loveValue); //�÷��̾�� �������ֱ�
            }
            else
            {
                PlayerPrefs.SetInt($"{character.characterName}Love", ((int)field.GetValue(GameManager.Instance.PlayerStat))); //�� �����ϱ�. (�ǻ��� �ҷ�����)
                PlayerPrefs.Save();
                loveValue = GameManager.Instance.CharacterLoveValue(character.characterName);
                LoveUp(0);
            }
        }
    }

    private void OnDisable()
    {
        GameManager.OnStart -= LoadData;
        LoadCard.OnLoad += LoadData;
    }
}
