using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.ComponentModel;
using System;

public class ChatSetting : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI characterName; //이름
    [SerializeField] private Image characterImage; //사진
    [SerializeField] private Slider characterLoveGauge; //러브 게이지
    [SerializeField] private TextMeshProUGUI characterLoveText; //러브 게이지

    public void CurrentCharacter(CharacterSO current) //첫 세팅
    {
        characterName.text = Name(current.characterName);
        //characterImage.sprite = character.characterImage;
       if(current.characterName != CharacterName.me)
        {
            characterLoveGauge.gameObject.SetActive(true);
            characterLoveText.gameObject.SetActive(true);
            characterLoveGauge.value = GameManager.Instance.CharacterLoveValue(current.characterName);
            characterLoveText.text = GameManager.Instance.CharacterLoveValue(current.characterName).ToString();
        }
        else
        {
            characterLoveGauge.gameObject.SetActive(false);
            characterLoveText.gameObject.SetActive(false);
        }
    }

    public static string Name<T>(T wantName)
    {
        var field = wantName.GetType().GetField(wantName.ToString());
        var attr = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
        return attr?.Description??wantName.ToString();
    }
}
