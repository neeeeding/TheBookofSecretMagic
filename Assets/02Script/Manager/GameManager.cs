using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> 
{
    public PlayerStatSC PlayerStat;
    public ItemSO[] ItemSO;

    public Player Player;

    public static Action CoinText;

    [ContextMenu("ResetDate")]
    public void ResetDate()
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

        AwakeDate();

        StartCoroutine(nowDate());
    }
    public void AddCoin(int num)
    {
        PlayerStat.playerCoin += num;
        CoinText?.Invoke();
        PlayerPrefs.SetInt("Coin", PlayerStat.playerCoin);
        PlayerPrefs.Save();
    }

    private void AwakeDate()
    {
        PlayerStat.year = PlayerPrefs.GetInt("Year");
        PlayerStat.month = PlayerPrefs.GetInt("Month");
        PlayerStat.day = PlayerPrefs.GetInt("Day");
        PlayerStat.hour = PlayerPrefs.GetInt("Hour");
        PlayerStat.minute = PlayerPrefs.GetInt("Minute");
        PlayerStat.playerCoin = PlayerPrefs.GetInt("Coin");
    }

    private IEnumerator nowDate()
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

    private bool CompareMonth(int day, int month)
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

