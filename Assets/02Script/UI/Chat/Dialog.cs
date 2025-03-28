using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private ChatSetting setting; //세팅 해주는 거
    [SerializeField] private TextMeshProUGUI dialogText; //대화
    [SerializeField] private TextMeshProUGUI[] selectTexts; //선택지 대화
    [Space(20f)]
    [SerializeField] private CharacterSO[] allCharacter; //모든 캐릭터의 정보. (호감도 못 올림)
    [Space(50f)]
    [SerializeField] private Dictionary<DialogPosition, Vector2> characterPosition; //위치 지정

    private List<Dictionary<string,object>> dialog; //csv 대화
    private int currentChapter; //현재 챕터
    private int currentNum; //현재 번호

    private Character currentCharacter; //다음 대화임을 알려주려고
    private CharacterSO currentSO; //정보
    public void DialogSetting(CharacterSO so, Character character) //세팅 해주기
    {
        currentCharacter = character;
        currentSO = so;
        int[] nums = character.CurrentDialog();
        currentChapter = nums[0];
        currentNum = nums[1];
        GetDialog();
    }

    private void GetDialog() //대화 얻기. (List)
    {
        TextAsset currentDialog = currentSO.characterDialog[0];
        dialog = CSVReader.Read(currentDialog);
        IsHoldItem();
    }

    private void IsHoldItem() //들고 있는 아이템
    {
        ItemSO so = GameManager.Instance.Item;
        if (so != null)
        {
            for(int i = 0; i< dialog.Count - 1; i++)
            {
                print($"{dialog[i][DialogType.Item.ToString()]}");
                print($"{so.itemType.ToString()}");
                if (dialog[i][DialogType.Item.ToString()].ToString() == so.itemType.ToString()) //대화의 아이템 창과 들고 있는 아이템 찾기
                {
                    print("ㅇ");
                }
            }
        }
    }

    private void DoChat(int dialogNum) //대화를 위한 배열 돌기
    {
        setting.CurrentCharacter(currentSO);
        dialogText.text = $"{dialog[0][DialogType.Text.ToString()]}";
    }

    private T GetEnum<T>(/*out*/ T wantEnum, string value) where T : struct, Enum //enum 얻는거
    {
        //if( Enum.TryParse(value, out wantEnum)) //존재 하는지
        //{
            return (T)Enum.Parse(typeof(T), value);
        //}
        //else
        //{
        //    return ;
        //}

        //return Enum.TryParse(value, out wantEnum);
    }
}

public enum DialogType
{
        Select, //선택지 인지
        ItemCategory, //아이템 카테고리
        Item, //아이템 종류
        GetLove, //얻는 호감도
        Chapter, //해당 챕터 (한 대화)
        Num, //챕터의 세부 번호
        Text, //대화
        Player, //대화 하는 캐릭터
        Position, //대화 하는 캐릭터의 위치
        OtherPosition, //이전 대화의 캐릭터의 위치
        SkipNum, //스킵 했을 때 넘어가는 번호
        NextNum //다음으로 넘어갈 번호

}

public enum DialogPosition
{ 
    none,
    right,
    left,
    middle
}
