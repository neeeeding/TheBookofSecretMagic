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
    public static string GameSaveFilePath; //파일 위치
    public string GamePath = "gameSaveData"; // 저장 경로

    public static Action CoinText;  //코인 수 갱신 (텍스트)
    public static Action OnStart; //모든 초기화 완료 후

    [Header("Setting")]
    public GameSaveData saveData; //기기에서만 저장 되는 것들 (ex: 저장 안한 진행사항)
    public PlayerStatSC PlayerStat; //플레이어 정보
    public Player Player; //플레이어 (state 조정 해줌(?))
    [Space(20f)]
    public ItemSO Item; //들고 있는 아이템?
    [Space(10f)]
    public bool isStart;
    [Space(50f)]
    [Header("Item")]
    public Dictionary<ItemType, int> Items = new Dictionary<ItemType, int>();

    [ContextMenu("ResetDate")]
    public void ResetDate() //초기화 하기
    {
       PlayerPrefs.DeleteAll();

        PlayerPrefs.SetInt("Year", 1);
        PlayerPrefs.SetInt("Month", 1);
        PlayerPrefs.SetInt("Day", 1);
        PlayerPrefs.SetInt("Hour", 1);
        PlayerPrefs.SetInt("Minute", 0);
        PlayerPrefs.SetInt("Coin", 5);

        PlayerPrefs.Save();
    }

    private void Awake()
    {
        //로드
        string jsson = PlayerPrefs.GetString(GamePath);
        GameSaveData data = JsonUtility.FromJson<GameSaveData>(jsson);
        saveData = data;

        GameSaveFilePath = Application.persistentDataPath + "/Save";
        print(GameSaveFilePath);
        isStart = false;
        PlayerStat = new PlayerStatSC();
        Player = gameObject.GetComponent<Player>();

        PlayerStat.ResetStat();

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

        //정보 저장
        saveData.stat = PlayerStat;

        string json = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString(GamePath, json);
        PlayerPrefs.Save();
    }

    public FieldInfo FindCharacterLastText(PlayerStatSC sc,CharacterName character) //마지막 텍스트 그거 반환
    {
        FieldInfo field = sc.GetType().GetField($"{character}LastText", BindingFlags.Public | BindingFlags.Instance);
        if(field != null && field.FieldType == typeof(int[]))
        {
            return field;
        }
        return null;
    }

    private void LoadData() //로드하기
    {
        PlayerPrefs.SetInt("Year", PlayerStat.year);
        PlayerPrefs.SetInt("Month", PlayerStat.month);
        PlayerPrefs.SetInt("Day", PlayerStat.day);
        PlayerPrefs.SetInt("Hour", PlayerStat.hour);
        PlayerPrefs.SetInt("Minute", PlayerStat.minute);
        PlayerPrefs.SetInt("Coin", PlayerStat.playerCoin);

        //호감도는 LikeabilityCard에서 해줌.

        AddItems(true);
        PlayerPrefs.Save();
    }

    public void SetLove(CharacterSO character, int love) //정보 넣고 해당 호감도 스탯에서의 이름 찾아서 전해주기
    {
        FieldInfo field = PlayerStat.GetType().GetField(character.characterName.ToString(), BindingFlags.Public | BindingFlags.Instance);
        if (field != null && field.FieldType == typeof(int))
        {
            field.SetValue(PlayerStat, (int)field.GetValue(PlayerStat) + love); //저장해주기
            PlayerPrefs.SetInt($"{character.characterName}Love", (int)field.GetValue(PlayerStat)); //값 저장하기. (실상은 불러오기)
            PlayerPrefs.Save();
        }
    }

    public int CharacterLoveValue(CharacterName character) //호감도 값 반환
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

    public void AddCoin(int num) //코인 수
    {
        PlayerStat.playerCoin += num;
        CoinText?.Invoke();
        PlayerPrefs.SetInt("Coin", PlayerStat.playerCoin);
        PlayerPrefs.Save();
    }

    public void AddItemCount(ItemCategory category,ItemType type,int num) //얻은, 잃은 아이템 수들
    {
        Items[type] += num;

        FieldInfo field = GetField(category, type);

        if(field != null)
        {
            field.SetValue(PlayerStat, Items[type]);
            PlayerPrefs.SetInt($"{type}", Items[type]);
        }
    }

    private void AwakeData() //값 세팅
    {
        //날짜
        PlayerStat.year = PlayerPrefs.GetInt("Year");
        PlayerStat.month = PlayerPrefs.GetInt("Month");
        PlayerStat.day = PlayerPrefs.GetInt("Day");
        PlayerStat.hour = PlayerPrefs.GetInt("Hour");
        PlayerStat.minute = PlayerPrefs.GetInt("Minute");
        PlayerStat.playerCoin = PlayerPrefs.GetInt("Coin");

        //능력치
        PlayerStat.potionMagic = saveData.stat.potionMagic;
        PlayerStat.copyMagic =   saveData.stat.copyMagic;
        PlayerStat.waterMagic =  saveData.stat.waterMagic;
        PlayerStat.fireMagic =   saveData.stat.fireMagic;
        PlayerStat.healMagic =   saveData.stat.healMagic;
        PlayerStat.blackMagic = saveData.stat.blackMagic;

        //호감도
        PlayerStat.resty = PlayerPrefs.GetInt($"{CharacterName.resty}Love");
        PlayerStat.chris = PlayerPrefs.GetInt($"{CharacterName.chris}Love");
        PlayerStat.theo = PlayerPrefs.GetInt($"{CharacterName.theo}Love");
        PlayerStat.pio = PlayerPrefs.GetInt($"{CharacterName.pio}Love");
        PlayerStat.noah = PlayerPrefs.GetInt($"{CharacterName.noah}Love");
        PlayerStat.nia = PlayerPrefs.GetInt($"{CharacterName.nia}Love");
        PlayerStat.villain = PlayerPrefs.GetInt($"{CharacterName.villain}Love");
        PlayerStat.harry = PlayerPrefs.GetInt($"{CharacterName.harry}Love");
        PlayerStat.daniel = PlayerPrefs.GetInt($"{CharacterName.daniel}Love");

        //아이템
        AddItems(false);
    }

    private FieldInfo GetField(ItemCategory category, ItemType type) //아이템변수들을 필드 찾기(stat에서)
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

    private void AddItems(bool load)//아이템 전부 추가
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
                    PlayerPrefs.SetInt($"{type}", (int)field.GetValue(PlayerStat));
                }
                field.SetValue(PlayerStat, PlayerPrefs.GetInt($"{type}"));
                Items.Add(type, (int)field.GetValue(PlayerStat));
            }
        }

    }

    private IEnumerator nowDate() //시간세는거
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);

            PlayerStat.minute++;
            PlayerPrefs.SetInt("Minute", PlayerStat.minute);
            if (PlayerStat.minute >= 60)
            {
                PlayerPrefs.SetInt("Hour", PlayerStat.hour);
                PlayerStat.minute = 0;
                PlayerStat.hour++;

                if(PlayerStat.hour >= 24)
                {
                    PlayerPrefs.SetInt("Day", PlayerStat.day);
                    PlayerStat.hour = 1;
                    PlayerStat.day++;

                    if(CompareMonth(PlayerStat.day, PlayerStat.month))
                    {
                        PlayerPrefs.SetInt("Month", PlayerStat.month);
                        PlayerStat.day = 1;
                        PlayerStat.month++;

                        if(PlayerStat.month > 12)
                        {
                            PlayerPrefs.SetInt("Year", PlayerStat.year);
                            PlayerStat.year++;
                        }
                    }

                }
            }
        }
    }

    private bool CompareMonth(int day, int month) //월 계산
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

