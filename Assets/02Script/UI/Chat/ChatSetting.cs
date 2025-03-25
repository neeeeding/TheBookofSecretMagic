using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.ComponentModel;
using System;

public class ChatSetting : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI characterName; //�̸�
    [SerializeField] private Image characterImage; //����
    [SerializeField] private Slider characterLoveGauge; //���� ������
    [SerializeField] private TextMeshProUGUI characterLoveText; //���� ������

    private CharacterSO character; //����

    public void CurrentCharacter(CharacterSO current) //ù ����
    {
        character = current;
        characterName.text = Name(current.characterName);
        //characterImage.sprite = character.characterImage;
        characterLoveGauge.value = GameManager.Instance.CharacterLoveValue(character.characterName);
        characterLoveText.text = GameManager.Instance.CharacterLoveValue(character.characterName).ToString();
    }

    public string Name<T>(T wantName)
    {
        var field = wantName.GetType().GetField(wantName.ToString());
        var attr = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
        return attr?.Description??wantName.ToString();
    }
}
