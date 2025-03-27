using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    private TextAsset currentDialog;
    private int currentChapter; //���� é��
    private int currentNum; //���� ��ȣ

    private Character currentCharacter;
    private CharacterSO currentSO;
    public void DialogSetting(CharacterSO so, Character character) //���� ���ֱ�
    {
        currentCharacter = character;
        currentSO = so;
        int[] nums = character.CurrentDialog();
        currentChapter = nums[0];
        currentNum = nums[1];
    }
}
