using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using TextAsset = UnityEngine.TextAsset;

public class Dialog : MonoBehaviour
{
    public static Action OnGame; //채팅 끝나면

    [Header("Component")]
    [SerializeField] private ChatSetting setting; //세팅 해주는 거
    [SerializeField] private TextMeshProUGUI dialogText; //대화
    [SerializeField] private TextMeshProUGUI[] selectTexts; //선택지 대화
    [Space(20f)]
    [SerializeField] private CharacterSO[] allCharacter; //모든 캐릭터의 정보. (호감도 못 올림)
    [Space(50f)]
    [SerializeField] private Dictionary<DialogPosition, Vector2> characterPosition; //위치 지정

    [Header("CurrentChapter")]
    private List<Dictionary<string,object>> dialog; //csv 대화
    [SerializeField]private int currentChapter; //현재 챕터
    [SerializeField]private int currentNum; //현재 번호
    [SerializeField]private int currentChat; //현재 CSV의 배열

    private Character currentCharacter; //다음 대화임을 알려주려고
    private CharacterSO currentSO; //정보

    public void DialogSetting(CharacterSO so, Character character) //세팅 해주기
    {
        currentCharacter = character;
        currentSO = so;
        int[] nums = character.CurrentDialog();
        currentChapter = nums[0];
        currentNum = nums[1];
        OffSelectText(); //선택지 텍스트 일단 다 끄기
        GetDialog();
        DoChat();
    }

    public void ClickNext() //다음으로
    {
        if(!HaveSelect(currentChat))
        {
            if (DoChat())
            {
                UISettingManager.Instance.CloseChat();
                OnGame?.Invoke();
            }
        }
    }

    private void GetDialog() //대화 (챕터 번호) 얻기. (List)
    {
        TextAsset currentDialog = currentSO.characterDialog[0];
        dialog = CSVReader.Read(currentDialog);
        IsHoldItem();

        //CSV 배열 찾기
        for (int i = 0; i < dialog.Count - 1; i++)
        {
            if (dialog[i][DialogType.Chapter.ToString()].ToString() == currentChapter.ToString()
                && dialog[i][DialogType.Num.ToString()].ToString() == currentNum.ToString()) //해당 배열의 수가 챕터랑 번호가 같으면
            {
                currentChat = i; //해당 배열의 수
                break;
            }
        }
    }

    private void IsHoldItem() //들고 있는 아이템 있다면 (챕터 번호)
    {
        ItemSO so = GameManager.Instance.Item;
        if (so != null)
        {
            for(int i = 0; i< dialog.Count - 1; i++)
            {
                if (dialog[i][DialogType.Item.ToString()].ToString() == so.itemType.ToString()) //대화의 아이템 창과 들고 있는 아이템 찾기
                {
                    currentChapter = (int)dialog[i][DialogType.Chapter.ToString()];
                    GameManager.Instance.AddItemCount(so.category, so.itemType, -1); //아이템 빼기
                    break;
                }
            }
        }
    }

    private bool DoChat() //대화를 위한 배열 돌기
    {
        bool getOut = true;

        //대화가 존재하는지 (배열 확인)
        if (dialog[currentChapter][DialogType.Chapter.ToString()].ToString() == currentChapter.ToString()
                       && dialog[currentChapter][DialogType.Num.ToString()].ToString() == currentNum.ToString()) //해당 배열의 수가 챕터랑 번호가 같으면
        {
            getOut = false;
            LoveUP(currentNum);
        }
        else
            getOut = true;


        setting.CurrentCharacter(currentSO); //세팅 해주는 거
        dialogText.text = dialog[currentChat][DialogType.Text.ToString()].ToString(); // 채팅 대화 입력하기
        currentNum++;
        return getOut;
    }

    private void LoveUP(int i) //호감도 오르거나 내리는 거 있으면 해주기.
    {
        if (dialog[i][DialogType.GetLove.ToString()].ToString() != "") //호감도 얻는게 있다면. (혹은 뺏는거)
        {
            int value = (int)dialog[i][DialogType.GetLove.ToString()];
            GameManager.Instance.SetLove(currentSO, value);
        }
    }

    private void OffSelectText() // 선택지 텍스트 다 꺼주기
    {
        foreach (var item in selectTexts)
        {
            item.gameObject.transform.parent.gameObject.SetActive(false);
        }
    }

    private bool HaveSelect(int i) //선택지가 있는지 (있으면 개수만 큼 켜줌.)
    {
        bool haveSelec = false; //false : 선택지 없음 (다음 번호)/ true : 선택지 있음 (건너뛰어서)

        OffSelectText(); //일단 다 끄기

        if (dialog[i][DialogType.Select.ToString()].ToString() != "") //선택지가 있다면
        {
            haveSelec = true;
            int count = (int)dialog[i][DialogType.Select.ToString()];
            for(int j = 0; j < count && j < selectTexts.Length; j++) //반복문
            {
                selectTexts[j].gameObject.transform.parent.gameObject.SetActive(true);

                int selectNumText = (int)dialog[i][DialogType.SelectText.ToString() + (j + 1)] - 1; //선택지에 사용될 텍스트 (번호) 찾기

                selectTexts[j].text = dialog[selectNumText][DialogType.Text.ToString()].ToString(); //해당 번째의 텍스트 입력
            }
        }

        return haveSelec;
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
        NextNum, //다음으로 넘어갈 번호
        SelectText
}

public enum DialogPosition
{ 
    none,
    right,
    left,
    middle
}
