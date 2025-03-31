using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    public static Action OnGame; //ä�� ������

    [Header("Component")]
    [SerializeField] private ChatSetting setting; //���� ���ִ� ��
    [SerializeField] private TextMeshProUGUI dialogText; //��ȭ
    [SerializeField] private TextMeshProUGUI[] selectTexts; //������ ��ȭ
    [Space(20f)]
    [SerializeField] private CharacterSO[] allCharacter; //��� ĳ������ ����. (ȣ���� �� �ø�)
    [Space(50f)]
    [SerializeField] private Dictionary<DialogPosition, Vector2> characterPosition; //��ġ ����

    private List<Dictionary<string,object>> dialog; //csv ��ȭ
    [SerializeField]private int currentChapter; //���� é��
    [SerializeField]private int currentNum; //���� ��ȣ

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

    public void ClickNext() //��������
    {
        if( DoChat())
        {
            UISettingManager.Instance.CloseChat();
            OnGame?.Invoke();
        }
    }

    private void GetDialog() //��ȭ (é�� ��ȣ) ���. (List)
    {
        TextAsset currentDialog = currentSO.characterDialog[0];
        dialog = CSVReader.Read(currentDialog);
        IsHoldItem();
    }

    private void IsHoldItem() //��� �ִ� ������ �ִٸ�
    {
        ItemSO so = GameManager.Instance.Item;
        if (so != null)
        {
            for(int i = 0; i< dialog.Count - 1; i++)
            {
                if (dialog[i][DialogType.Item.ToString()].ToString() == so.itemType.ToString()) //��ȭ�� ������ â�� ��� �ִ� ������ ã��
                {
                    currentChapter = (int)dialog[i][DialogType.Chapter.ToString()];
                    GameManager.Instance.AddItemCount(so.category, so.itemType, -1);
                    break;
                }
            }
        }
    }

    private bool DoChat() //��ȭ�� ���� �迭 ����
    {
        bool getOut = true;
        setting.CurrentCharacter(currentSO);
        int currentChat = 0;
        for(int i = 0; i < dialog.Count - 1; i++)
        {
            if (dialog[i][DialogType.Chapter.ToString()].ToString() == currentChapter.ToString() && dialog[i][DialogType.Num.ToString()].ToString() == currentNum.ToString()) //é�Ͷ� ��ȣ�� ������
            {
                currentChat = i;
                getOut = false;
                break;
            }
            else
            {
                getOut = true;
            }
        }
        dialogText.text = dialog[currentChat][DialogType.Text.ToString()].ToString();
        currentNum++;
        return getOut;
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
