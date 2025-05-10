using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.U2D.Aseprite;
using UnityEngine.TextCore.Text;
using System.IO;
using Unity.VisualScripting;

public class GameManager : Singleton<GameManager>
{
    public static string GameSaveFilePath; //���� ��ġ
    public string GamePath = "gameSaveData"; // ���� ���

    public static Action CoinText;  //���� �� ���� (�ؽ�Ʈ)
    public static Action OnStart; //��� �ʱ�ȭ �Ϸ� ��

    [Header("Setting")]
    public GameSaveData saveData; //��⿡���� ���� �Ǵ� �͵� (ex: ���� ���� �������)
    public PlayerStatSC PlayerStat; //�÷��̾� ����
    public Player Player; //�÷��̾� (state ���� ����(?))
    [Space(20f)]
    public ItemSO Item; //��� �ִ� ������?
    [Space(10f)]
    public bool isStart;

    [ContextMenu("ResetAll")]
    public void ResetDate() //�ʱ�ȭ �ϱ�
    {
       PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    private void Awake()
    {
        //�ε�
        string jsson = PlayerPrefs.GetString(GamePath);
        GameSaveData data = JsonUtility.FromJson<GameSaveData>(jsson);
        saveData = data;

        GameSaveFilePath = Application.persistentDataPath + "/Save";
        print(GameSaveFilePath);
        isStart = false;

        PlayerStat = data.stat; //�ε�

        Player = gameObject.GetComponent<Player>();


        //PlayerStat.ResetStat();
        //ResetValue();

        LoadCard.OnLoad += LoadData;
        ItemCard.OnHoldItem += hold => Item = hold;

        StartCoroutine(nowDate());
    }

    private void Start()
    {
        OnStart?.Invoke();
        isStart = true;
    }

    private void OnDisable()
    {
        LoadCard.OnLoad -= LoadData;

        //���� ����
        saveData.stat = PlayerStat;
        

        string json = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString(GamePath, json);
        PlayerPrefs.Save();
    }

    private void LoadData() //�ε��ϱ�
    {
        //ȣ������ LikeabilityCard���� ����.

        //�ε��� �� ��
    }

    public void SetLove(CharacterSO character, int love) //���� �ְ� �ش� ȣ���� ���ȿ����� �̸� ã�Ƽ� �����ֱ�
    {
        FieldInfo field = PlayerStat.GetType().GetField(character.characterName.ToString(), BindingFlags.Public | BindingFlags.Instance);
        if (field != null && field.FieldType == typeof(int))
        {
            int.TryParse(PlayerStat.characterlastText[character.characterName][DialogType.Love], out int basic); //���� �� ��������

            PlayerStat.characterlastText[character.characterName][DialogType.Love] = (basic + love).ToString(); //�������ֱ�
        }
    }

    public void AddCoin(int num) //���� ��
    {
        PlayerStat.playerCoin += num;
        CoinText?.Invoke();
    }

    public void AddItemCount(ItemCategory category,ItemType type,int num) //����, ���� ������ ����
    {
        PlayerStat.items[category][type] += num;
    }

    private void ResetValue() //�� ����
    {
        //ȣ����
        ResetCharacter();

        //������
        ResetItem();
    }

    private void ResetCharacter() //ĳ���͵�  ���� �ʱ�ȭ
    {
        PlayerStat.characterlastText = new SaveDictionary<CharacterName, SaveDictionary<DialogType, string>>();
        PlayerStat.characterlastText.Clear();

        int num;

        foreach (CharacterName name in Enum.GetValues(typeof(CharacterName))) //�̸��� ����
        {
            num = (int)name / 1000;
            SaveDictionary<DialogType, string> di = new SaveDictionary<DialogType, string>();

            foreach (DialogType dialog in Enum.GetValues(typeof(DialogType))) //��� �� ���� / ���̾�α� ���� (é��, �ѹ�, �ؽ�Ʈ, �޸�, ���� �� ����ϱ� ��.)
            {
                di.Add(dialog, ""); // " " �ʱ�ȭ
            }
            PlayerStat.characterlastText.Add(name, di); //����
        }
    }

    private void ResetItem() //������ ������ ���� �ʱ�ȭ
    {
        PlayerStat.items = new SaveDictionary<ItemCategory, SaveDictionary<ItemType, int>>();
        PlayerStat.items.Clear();

        int num;

        foreach(ItemCategory category in Enum.GetValues(typeof(ItemCategory))) //ī�װ� ����
        {
            if (category == ItemCategory.coin || category == ItemCategory.none) //���� ����
                continue;

            num = (int)category/1000;
            SaveDictionary<ItemType, int> item = new SaveDictionary<ItemType, int>(); //������

            foreach (ItemType type in Enum.GetValues(typeof(ItemType))) //�ش� ī�װ��� �� �ڸ� ���� ���Ḧ ����
            {
                if (num != (int)type / 1000) //���ڸ� ��
                    continue;

                item.Add(type, 0); //0���� �ʱ�ȭ
            }
            PlayerStat.items.Add(category, item); //����
        }
    }

    private IEnumerator nowDate() //�ð����°�
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);

            PlayerStat.minute++;
            if (PlayerStat.minute >= 60)
            {
                PlayerStat.minute = 0;
                PlayerStat.hour++;

                if(PlayerStat.hour >= 24)
                {
                    PlayerStat.hour = 1;
                    PlayerStat.day++;

                    if(CompareMonth(PlayerStat.day, PlayerStat.month))
                    {
                        PlayerStat.day = 1;
                        PlayerStat.month++;

                        if(PlayerStat.month > 12)
                        {
                            PlayerStat.year++;
                        }
                    }

                }
            }
        }
    }

    private bool CompareMonth(int day, int month) //�� ���
    {
        if (day < 28) return false;

        int[] day31 = { 1, 3, 5, 7, 8, 10, 12 };
        int[] day30 = { 4, 6, 9, 11};

        if((day > 28 && month == 2) || (day > 30 && Array.Exists(day30, x => x == month)) || (day > 31 && Array.Exists(day31, x => x == month)))
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}

