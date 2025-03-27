using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    private TextAsset currentDialog;
    private int currentChapter; //현재 챕터
    private int currentNum; //현재 번호

    private Character currentCharacter;
    private CharacterSO currentSO;
    public void DialogSetting(CharacterSO so, Character character) //세팅 해주기
    {
        currentCharacter = character;
        currentSO = so;
        int[] nums = character.CurrentDialog();
        currentChapter = nums[0];
        currentNum = nums[1];
    }
}
