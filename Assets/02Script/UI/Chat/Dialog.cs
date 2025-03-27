using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private ChatSetting setting; //세팅 해주는 거
    [SerializeField] private TextMeshProUGUI dialogText; //대화
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
        DoChat();
    }

    private void GetDialog() //대화 얻기. (List)
    {
        TextAsset currentDialog = currentSO.characterDialog[0];
        dialog = CSVReader.Read(currentDialog.name);
    }

    private void DoChat() //대화 하기
    {
        setting.CurrentCharacter(currentSO);
        dialogText.text = $"{dialog[0][DialogType.Text.ToString()]}";
    }
}

public enum DialogType
{
    Chapter,
    Num,
    Text,
    Player,
    Position,
    OtherPosition
}

public enum DialogPosition
{ 
    none,
    right,
    left,
    middle
}
