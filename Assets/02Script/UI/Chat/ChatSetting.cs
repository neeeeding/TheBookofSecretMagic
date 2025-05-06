using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.ComponentModel;
using System;
using UnityEngine.TextCore.Text;

public class ChatSetting : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI characterName; //�̸�
    [SerializeField] private Image characterImage; //����
    [SerializeField] private Slider characterLoveGauge; //���� ������
    [SerializeField] private TextMeshProUGUI characterLoveText; //���� ������

    public void CurrentCharacter(CharacterSO current) //ù ����
    {
        characterName.text = Name(current.characterName);
        //characterImage.sprite = character.characterImage;
       if(current.characterName != CharacterName.me)
        {
            characterLoveGauge.gameObject.SetActive(true);
            characterLoveText.gameObject.SetActive(true);

            int.TryParse(GameManager.Instance.PlayerStat.characterlastText[current.characterName][DialogType.Love], out int love);
            characterLoveGauge.value = love;

            characterLoveText.text = GameManager.Instance.PlayerStat.characterlastText[current.characterName][DialogType.Text];
        }
        else
        {
            characterLoveGauge.gameObject.SetActive(false);
            characterLoveText.gameObject.SetActive(false);
        }
    }

    public static string Name<T>(T wantName) //enum�� �ּ�? ���
    {
        var field = wantName.GetType().GetField(wantName.ToString());
        var attr = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
        return attr?.Description??wantName.ToString();
    }
}
