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
    public static Action OnGame; //ä�� ������

    [Header("Component")]
    [SerializeField] private ChatSetting setting; //���� ���ִ� ��
    [SerializeField] private TextMeshProUGUI dialogText; //��ȭ
    [SerializeField] private TextMeshProUGUI[] selectTexts; //������ ��ȭ
    [Space(20f)]
    [SerializeField] private CharacterSO[] allCharacter; //��� ĳ������ ����. (ȣ���� �� �ø�)
    [Space(50f)]
    [SerializeField] private Dictionary<DialogPosition, Vector2> characterPosition; //��ġ ����

    [Header("CurrentChapter")]
    private List<Dictionary<string,object>> dialog; //csv ��ȭ
    [SerializeField]private int currentChapter; //���� é��
    [SerializeField]private int currentNum; //���� ��ȣ
    [SerializeField]private int currentChat; //���� CSV�� �迭

    private Character currentCharacter; //���� ��ȭ���� �˷��ַ���
    private CharacterSO currentSO; //����

    public void DialogSetting(CharacterSO so, Character character) //���� ���ֱ�
    {
        currentCharacter = character;
        currentSO = so;
        int[] nums = character.CurrentDialog();
        currentChapter = nums[0];
        currentNum = nums[1];
        OffSelectText(); //������ �ؽ�Ʈ �ϴ� �� ����
        GetDialog();
        DoChat();
    }

    public void ClickNext() //��������
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

    private void GetDialog() //��ȭ (é�� ��ȣ) ���. (List)
    {
        TextAsset currentDialog = currentSO.characterDialog[0];
        dialog = CSVReader.Read(currentDialog);
        IsHoldItem();

        //CSV �迭 ã��
        for (int i = 0; i < dialog.Count - 1; i++)
        {
            if (dialog[i][DialogType.Chapter.ToString()].ToString() == currentChapter.ToString()
                && dialog[i][DialogType.Num.ToString()].ToString() == currentNum.ToString()) //�ش� �迭�� ���� é�Ͷ� ��ȣ�� ������
            {
                currentChat = i; //�ش� �迭�� ��
                break;
            }
        }
    }

    private void IsHoldItem() //��� �ִ� ������ �ִٸ� (é�� ��ȣ)
    {
        ItemSO so = GameManager.Instance.Item;
        if (so != null)
        {
            for(int i = 0; i< dialog.Count - 1; i++)
            {
                if (dialog[i][DialogType.Item.ToString()].ToString() == so.itemType.ToString()) //��ȭ�� ������ â�� ��� �ִ� ������ ã��
                {
                    currentChapter = (int)dialog[i][DialogType.Chapter.ToString()];
                    GameManager.Instance.AddItemCount(so.category, so.itemType, -1); //������ ����
                    break;
                }
            }
        }
    }

    private bool DoChat() //��ȭ�� ���� �迭 ����
    {
        bool getOut = true;

        //��ȭ�� �����ϴ��� (�迭 Ȯ��)
        if (dialog[currentChapter][DialogType.Chapter.ToString()].ToString() == currentChapter.ToString()
                       && dialog[currentChapter][DialogType.Num.ToString()].ToString() == currentNum.ToString()) //�ش� �迭�� ���� é�Ͷ� ��ȣ�� ������
        {
            getOut = false;
            LoveUP(currentNum);
        }
        else
            getOut = true;


        setting.CurrentCharacter(currentSO); //���� ���ִ� ��
        dialogText.text = dialog[currentChat][DialogType.Text.ToString()].ToString(); // ä�� ��ȭ �Է��ϱ�
        currentNum++;
        return getOut;
    }

    private void LoveUP(int i) //ȣ���� �����ų� ������ �� ������ ���ֱ�.
    {
        if (dialog[i][DialogType.GetLove.ToString()].ToString() != "") //ȣ���� ��°� �ִٸ�. (Ȥ�� ���°�)
        {
            int value = (int)dialog[i][DialogType.GetLove.ToString()];
            GameManager.Instance.SetLove(currentSO, value);
        }
    }

    private void OffSelectText() // ������ �ؽ�Ʈ �� ���ֱ�
    {
        foreach (var item in selectTexts)
        {
            item.gameObject.transform.parent.gameObject.SetActive(false);
        }
    }

    private bool HaveSelect(int i) //�������� �ִ��� (������ ������ ŭ ����.)
    {
        bool haveSelec = false; //false : ������ ���� (���� ��ȣ)/ true : ������ ���� (�ǳʶپ)

        OffSelectText(); //�ϴ� �� ����

        if (dialog[i][DialogType.Select.ToString()].ToString() != "") //�������� �ִٸ�
        {
            haveSelec = true;
            int count = (int)dialog[i][DialogType.Select.ToString()];
            for(int j = 0; j < count && j < selectTexts.Length; j++) //�ݺ���
            {
                selectTexts[j].gameObject.transform.parent.gameObject.SetActive(true);

                int selectNumText = (int)dialog[i][DialogType.SelectText.ToString() + (j + 1)] - 1; //�������� ���� �ؽ�Ʈ (��ȣ) ã��

                selectTexts[j].text = dialog[selectNumText][DialogType.Text.ToString()].ToString(); //�ش� ��°�� �ؽ�Ʈ �Է�
            }
        }

        return haveSelec;
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
        NextNum, //�������� �Ѿ ��ȣ
        SelectText
}

public enum DialogPosition
{ 
    none,
    right,
    left,
    middle
}
