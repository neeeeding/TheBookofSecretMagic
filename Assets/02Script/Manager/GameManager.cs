using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> 
{
    public PlayerStatSC PlayerStat;
    public Player Player;
    public Dictionary<ItemType, int> Items = new Dictionary<ItemType, int>();

    public static Action CoinText;  //코인 수 갱신 (텍스트)

    [ContextMenu("ResetDate")]
    public void ResetDate() //초기화 하기
    {
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
        Player = gameObject.GetComponent<Player>();

        PlayerStat = new PlayerStatSC();
        PlayerStat.ResetStat();

        PlayerStat.playerSpeed = 1f;

        AwakeData();

        LoadCard.OnLoad += AddItems;

        StartCoroutine(nowDate());
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

        //변수 찾기
        string itemName = type.ToString();
        if(category != ItemCategory.mouse)
        {
            itemName += "Count";
        }
        FieldInfo field = PlayerStat.GetType().GetField(itemName, BindingFlags.Public | BindingFlags.Instance);
        if (field != null && field.FieldType == typeof(int))
        {
            field.SetValue(PlayerStat, Items[type]);
        }
    }

    private void AwakeData() //값 세팅
    {
        PlayerStat.year = PlayerPrefs.GetInt("Year");
        PlayerStat.month = PlayerPrefs.GetInt("Month");
        PlayerStat.day = PlayerPrefs.GetInt("Day");
        PlayerStat.hour = PlayerPrefs.GetInt("Hour");
        PlayerStat.minute = PlayerPrefs.GetInt("Minute");
        PlayerStat.playerCoin = PlayerPrefs.GetInt("Coin");

        AddItems();
    }

    private void AddItems()//아이템 전부 추가
    {
        Items.Clear();

        Items.Add(ItemType.lovePoition, PlayerStat.lovePoitionCount);
        Items.Add(ItemType.staminaPoition, PlayerStat.staminaPoitionCount);
        Items.Add(ItemType.painPoition, PlayerStat.painPoitionConut);

        Items.Add(ItemType.blackbook, PlayerStat.blackbookCount);
        Items.Add(ItemType.healbook, PlayerStat.healbookCount);
        Items.Add(ItemType.firebook, PlayerStat.firebookCount);
        Items.Add(ItemType.waterbook, PlayerStat.waterbookCount);
        Items.Add(ItemType.copybook, PlayerStat.copybookCount);
        Items.Add(ItemType.potionbook, PlayerStat.potionbookCount);

        Items.Add(ItemType.umbrella, PlayerStat.umbrellaCount);
        Items.Add(ItemType.broomstick, PlayerStat.broomstickCount);
        Items.Add(ItemType.fan, PlayerStat.fanCount);
        Items.Add(ItemType.hotPack, PlayerStat.hotPackCount);
        Items.Add(ItemType.fryingPan, PlayerStat.fryingPanCount);
        Items.Add(ItemType.flower, PlayerStat.flowerCount);
        Items.Add(ItemType.foragingBin, PlayerStat.foragingBinCount);

        Items.Add(ItemType.drug, PlayerStat.drugCount);
        Items.Add(ItemType.box, PlayerStat.boxCount);
        Items.Add(ItemType.glasses, PlayerStat.glassesCount);
        Items.Add(ItemType.readingGlasses, PlayerStat.readingGlassesCount);
        Items.Add(ItemType.emptyGlass, PlayerStat.emptyGlassCount);

        Items.Add(ItemType.worm, PlayerStat.wormCount);
        Items.Add(ItemType.trashGlass, PlayerStat.trashGlassCount);
        Items.Add(ItemType.perfectGlass, PlayerStat.perfectGlassCount);

        Items.Add(ItemType.restyMouse, PlayerStat.restyMouse);
        Items.Add(ItemType.chrisMouse, PlayerStat.chrisMouse);
        Items.Add(ItemType.theoMouse, PlayerStat.theoMouse);
        Items.Add(ItemType.noahMouse, PlayerStat.noahMouse);
        Items.Add(ItemType.niaMouse, PlayerStat.niaMouse);
        Items.Add(ItemType.villainMouse, PlayerStat.villainMouse);
        Items.Add(ItemType.harryMouse, PlayerStat.harryMouse);
        Items.Add(ItemType.danielMouse, PlayerStat.danielMouse);
        Items.Add(ItemType.pioMouse, PlayerStat.pioMouse);

        Items.Add(ItemType.gift, PlayerStat.giftCount);
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

    private void OnDisable()
    {
        LoadCard.OnLoad -= AddItems;
    }
}

