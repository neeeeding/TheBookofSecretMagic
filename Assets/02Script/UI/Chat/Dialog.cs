using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private ChatSetting setting; //���� ���ִ� ��
    [SerializeField] private TextMeshProUGUI dialogText; //��ȭ
    [SerializeField] private TextMeshProUGUI[] selectTexts; //������ ��ȭ
    [Space(20f)]
    [SerializeField] private CharacterSO[] allCharacter; //��� ĳ������ ����. (ȣ���� �� �ø�)
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
    }

    private void GetDialog() //��ȭ ���. (List)
    {
        TextAsset currentDialog = currentSO.characterDialog[0];
        dialog = CSVReader.Read(currentDialog);
        IsHoldItem();
    }

    private void IsHoldItem() //��� �ִ� ������
    {
        ItemSO so = GameManager.Instance.Item;
        if (so != null)
        {
            for(int i = 0; i< dialog.Count - 1; i++)
            {
                print($"{dialog[i][DialogType.Item.ToString()]}");
                print($"{so.itemType.ToString()}");
                if (dialog[i][DialogType.Item.ToString()].ToString() == so.itemType.ToString()) //��ȭ�� ������ â�� ��� �ִ� ������ ã��
                {
                    print("��");
                }
            }
        }
    }

    private void DoChat(int dialogNum) //��ȭ�� ���� �迭 ����
    {
        setting.CurrentCharacter(currentSO);
        dialogText.text = $"{dialog[0][DialogType.Text.ToString()]}";
    }

    private T GetEnum<T>(/*out*/ T wantEnum, string value) where T : struct, Enum //enum ��°�
    {
        //if( Enum.TryParse(value, out wantEnum)) //���� �ϴ���
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
        Select, //������ ����
        ItemCategory, //������ ī�װ�
        Item, //������ ����
        GetLove, //��� ȣ����
        Chapter, //�ش� é�� (�� ��ȭ)
        Num, //é���� ���� ��ȣ
        Text, //��ȭ
        Player, //��ȭ �ϴ� ĳ����
        Position, //��ȭ �ϴ� ĳ������ ��ġ
        OtherPosition, //���� ��ȭ�� ĳ������ ��ġ
        SkipNum, //��ŵ ���� �� �Ѿ�� ��ȣ
        NextNum //�������� �Ѿ ��ȣ

}

public enum DialogPosition
{ 
    none,
    right,
    left,
    middle
}
