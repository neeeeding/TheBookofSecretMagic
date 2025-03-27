using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private ChatSetting setting; //���� ���ִ� ��
    [SerializeField] private TextMeshProUGUI dialogText; //��ȭ
    [Space(50f)]
    [SerializeField] private Dictionary<DialogPosition, Vector2> characterPosition; //��ġ ����

    private List<Dictionary<string,object>> dialog; //csv ��ȭ
    private int currentChapter; //���� é��
    private int currentNum; //���� ��ȣ

    private Character currentCharacter; //���� ��ȭ���� �˷��ַ���
    private CharacterSO currentSO; //����
    public void DialogSetting(CharacterSO so, Character character) //���� ���ֱ�
    {
        currentCharacter = character;
        currentSO = so;
        int[] nums = character.CurrentDialog();
        currentChapter = nums[0];
        currentNum = nums[1];
        GetDialog();
        DoChat();
    }

    private void GetDialog() //��ȭ ���. (List)
    {
        TextAsset currentDialog = currentSO.characterDialog[0];
        dialog = CSVReader.Read(currentDialog.name);
    }

    private void DoChat() //��ȭ �ϱ�
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
