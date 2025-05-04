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
    [Space(50f)]
    [Header("Item")]
    public Dictionary<ItemType, int> Items = new Dictionary<ItemType, int>();

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
        PlayerStat = saveData.stat; //�ε�
        Player = gameObject.GetComponent<Player>();

        PlayerStat.ResetStat();
        ResetDate();

        AwakeData();

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

    public FieldInfo FindCharacterLastText(PlayerStatSC sc,CharacterName character) //������ �ؽ�Ʈ �װ� ��ȯ
    {
        FieldInfo field = sc.GetType().GetField($"{character}LastText", BindingFlags.Public | BindingFlags.Instance);
        if(field != null && field.FieldType == typeof(int[]))
        {
            return field;
        }
        return null;
    }

    private void LoadData() //�ε��ϱ�
    {
        //ȣ������ LikeabilityCard���� ����.


        AddItems(true);
    }

    public void SetLove(CharacterSO character, int love) //���� �ְ� �ش� ȣ���� ���ȿ����� �̸� ã�Ƽ� �����ֱ�
    {
        FieldInfo field = PlayerStat.GetType().GetField(character.characterName.ToString(), BindingFlags.Public | BindingFlags.Instance);
        if (field != null && field.FieldType == typeof(int))
        {
            field.SetValue(PlayerStat, (int)field.GetValue(PlayerStat) + love); //�������ֱ�
            PlayerPrefs.SetInt($"{character.characterName}Love", (int)field.GetValue(PlayerStat)); //�� �����ϱ�. (�ǻ��� �ҷ�����)
            PlayerPrefs.Save();
        }
    }

    public int CharacterLoveValue(CharacterName character) //ȣ���� �� ��ȯ
    {
        return character switch
        {
            CharacterName.resty => PlayerStat.resty,
            CharacterName.chris => PlayerStat.chris,
            CharacterName.theo => PlayerStat.theo,
            CharacterName.noah => PlayerStat.noah,
            CharacterName.nia => PlayerStat.nia,
            CharacterName.villain => PlayerStat.villain,
            CharacterName.harry => PlayerStat.harry,
            CharacterName.daniel => PlayerStat.daniel,
            CharacterName.pio => PlayerStat.pio,
            _=> 0
        };
    }

    public void AddCoin(int num) //���� ��
    {
        PlayerStat.playerCoin += num;
        CoinText?.Invoke();
        PlayerPrefs.SetInt("Coin", PlayerStat.playerCoin);
        PlayerPrefs.Save();
    }

    public void AddItemCount(ItemCategory category,ItemType type,int num) //����, ���� ������ ����
    {
        Items[type] += num;

        FieldInfo field = GetField(category, type);

        if(field != null)
        {
            field.SetValue(PlayerStat, Items[type]);
            PlayerPrefs.SetInt($"{type}", Items[type]);
        }
    }

    private void AwakeData() //�� ����
    {
        //ȣ����
        PlayerStat.resty = PlayerPrefs.GetInt($"{CharacterName.resty}Love");
        PlayerStat.chris = PlayerPrefs.GetInt($"{CharacterName.chris}Love");
        PlayerStat.theo = PlayerPrefs.GetInt($"{CharacterName.theo}Love");
        PlayerStat.pio = PlayerPrefs.GetInt($"{CharacterName.pio}Love");
        PlayerStat.noah = PlayerPrefs.GetInt($"{CharacterName.noah}Love");
        PlayerStat.nia = PlayerPrefs.GetInt($"{CharacterName.nia}Love");
        PlayerStat.villain = PlayerPrefs.GetInt($"{CharacterName.villain}Love");
        PlayerStat.harry = PlayerPrefs.GetInt($"{CharacterName.harry}Love");
        PlayerStat.daniel = PlayerPrefs.GetInt($"{CharacterName.daniel}Love");

        //������
        //AddItems(false);
    }

    private FieldInfo GetField(ItemCategory category, ItemType type) //�����ۺ������� �ʵ� ã��(stat����)
    {
        string itemName = type.ToString();
        if(category != ItemCategory.mouse)
        {
            itemName += "Count";
        }
        FieldInfo field = PlayerStat.GetType().GetField(itemName, BindingFlags.Public | BindingFlags.Instance);
        if(field != null && field.FieldType == typeof(int) && category != ItemCategory.coin)
        {
            return field;
        }
        return null;
    }

    private void ResetItem() //������ ������ ���� �ʱ�ȭ
    {
        PlayerStat.items.Clear();

        int num;

        foreach(ItemCategory category in Enum.GetValues(typeof(ItemCategory))) //ī�װ� ����
        {
            if (category != ItemCategory.coin) //���� ����
                continue;

            num = (int)category/1000;
            Dictionary<ItemType, int> item = new Dictionary<ItemType, int>(); //������

            foreach (ItemType type in Enum.GetValues(typeof(ItemType))) //�ش� ī�װ��� �� �ڸ� ���� ���Ḧ ����
            {
                if (num != (int)type / 1000) //���ڸ� ��
                    continue;

                item.Add(type, 0); //0���� �ʱ�ȭ
            }
            PlayerStat.items.Add(category, item); //����

        }
    }

    private void AddItems(bool load)//������ ���� �߰�
    {
        Items.Clear();

        foreach (ItemType type in Enum.GetValues(typeof(ItemType)))
        {
            ItemCategory category = type.ToString().Contains("mouse") ? ItemCategory.mouse : ItemCategory.none ;
            FieldInfo field = GetField(category,type);
            if (field != null)
            {
                if (load)
                {
                    //����
                }
                //�ε�
                Items.Add(type, (int)field.GetValue(PlayerStat));
            }
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

