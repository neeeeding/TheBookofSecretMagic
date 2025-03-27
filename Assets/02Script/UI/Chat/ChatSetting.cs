using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.ComponentModel;
using System;

public class ChatSetting : MonoBehaviour
{
    [SerializeField] private Dialog myDialog;

    [SerializeField] private TextMeshProUGUI characterName; //�̸�
    [SerializeField] private Image characterImage; //����
    [SerializeField] private Slider characterLoveGauge; //���� ������
    [SerializeField] private TextMeshProUGUI characterLoveText; //���� ������

    private Character character;
    private CharacterSO currentSO; //����

    public void CurrentCharacter(CharacterSO current, Character character) //ù ����
    {
        currentSO = current;
        this.character = character;
        characterName.text = Name(current.characterName);
        //characterImage.sprite = character.characterImage;
        characterLoveGauge.value = GameManager.Instance.CharacterLoveValue(currentSO.characterName);
        characterLoveText.text = GameManager.Instance.CharacterLoveValue(currentSO.characterName).ToString();
    }

    public static string Name<T>(T wantName)
    {
        var field = wantName.GetType().GetField(wantName.ToString());
        var attr = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
        return attr?.Description??wantName.ToString();
    }
}
