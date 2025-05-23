using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    //저장을 num : (챕터의) 넘버/ chapter : 챕터/ text : 마지막 대화
    //LikeabilityCard = 
    public static Action<CharacterSO,Character> OnChat;

    [SerializeField] private CharacterSO characterSO;
    [SerializeField] private int chapter; //챕터
    [SerializeField] private int finallNum; //번호
    private PlayerStatSC path; //스탯 (저장 공간)
    private bool isChat;

    private void Awake()
    {
        isChat = false;
        path = GameManager.Instance.PlayerStat;
        int.TryParse(path.characterlastText[characterSO.characterName][DialogType.Chapter],out chapter);
        int.TryParse(path.characterlastText[characterSO.characterName][DialogType.Num],out finallNum);
    }

    public void Load() //로드 될 때
    {
        path = GameManager.Instance.PlayerStat; // 

        if (path != null)
        {
            int.TryParse(path.characterlastText[characterSO.characterName][DialogType.Chapter], out chapter);
            int.TryParse(path.characterlastText[characterSO.characterName][DialogType.Num], out finallNum);
        }

        //아이템이나 특수 대화에서는 문제가 없는지 확인 할 것
        if (GameManager.Instance.PlayerStat.isChat)
        {
            UISettingManager.Instance.CloseChat();
            finallNum--; //대화를 시작 할 때 1를 추가하고 시작함으로.
            UISettingManager.Instance.Chat(GameManager.Instance.PlayerStat.lastSO, GameManager.Instance.PlayerStat.lastCharacter);
        }
        else
        {
            UISettingManager.Instance.CloseChat();
        }
    }

    public void NextDialog(int i) //대화가 진행 될 때마다
    {
        finallNum = i;

        path.characterlastText[characterSO.characterName][DialogType.Num] = finallNum.ToString();
    }

    public int[] CurrentDialog() //현재 진행 사항 (챕터, 넘버 값 넘겨주기)
    {
        return new int[]{chapter, finallNum};
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isChat = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isChat = false;
        }
    }

    public void NextChapter() //다음 챕터로 설정 해주기
    {
        finallNum = 1;
        chapter++;

        path.characterlastText[characterSO.characterName][DialogType.Chapter] = chapter.ToString();
        path.characterlastText[characterSO.characterName][DialogType.Num] = finallNum.ToString();
    }

    public void ClickCharacter() //대화 하기 (클릭)
    {
        if (isChat)
        {
            //finallNum = 1;
            OnChat?.Invoke(characterSO,this);
        }
    }

    private void OnEnable()
    {
        LoadCard.OnLoad += Load;
    }

    private void OnDisable()
    {
        LoadCard.OnLoad -= Load;
    }
}
