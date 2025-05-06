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

    [ContextMenu("ResetAll")]
    public void ResetDate() //초기화 하기
    {
       PlayerPrefs.DeleteAll();
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

        PlayerStat = saveData.stat; //로드
        //PlayerStat = new PlayerStatSC();

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

    private void LoadData() //로드하기
    {
        //호감도는 LikeabilityCard에서 해줌.

        //로드해 줄 것
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

    public void AddCoin(int num) //코인 수
    {
        PlayerStat.playerCoin += num;
        CoinText?.Invoke();
    }

    public void AddItemCount(ItemCategory category,ItemType type,int num) //얻은, 잃은 아이템 수들
    {
        PlayerStat.items[category][type] += num;
    }

    private void AwakeData() //값 세팅
    {
        //호감도

        //아이템
        ResetItem();
    }

    private void ResetItem() //스탯의 아이템 전부 초기화
    {
        PlayerStat.items = new SaveDictionary<ItemCategory, SaveDictionary<ItemType, int>>();
        PlayerStat.items.Clear();

        int num;

        foreach(ItemCategory category in Enum.GetValues(typeof(ItemCategory))) //카테고리 저장
        {
            if (category == ItemCategory.coin || category == ItemCategory.none) //코인 제외
                continue;

            num = (int)category/1000;
            SaveDictionary<ItemType, int> item = new SaveDictionary<ItemType, int>(); //아이템

            foreach (ItemType type in Enum.GetValues(typeof(ItemType))) //해당 카테고리와 앞 자리 같은 종료를 저장
            {
                if (num != (int)type / 1000) //앞자리 비교
                    continue;

                item.Add(type, 0); //0으로 초기화
            }
            PlayerStat.items.Add(category, item); //저장
        }
    }

    private IEnumerator nowDate() //시간세는거
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

