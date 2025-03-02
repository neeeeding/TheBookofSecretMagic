using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> 
{
    public PlayerStatSO PlayerStat;

    public Player Player;

    [ContextMenu("ResetDate")]
    public void ResetDate()
    {
        PlayerPrefs.SetInt("Year", 1);
        PlayerPrefs.SetInt("Month", 1);
        PlayerPrefs.SetInt("Day", 1);
        PlayerPrefs.SetInt("Hour", 1);
        PlayerPrefs.SetInt("Minute", 0);

        PlayerPrefs.Save();
    }

    private void Awake()
    {
        Player = gameObject.GetComponent<Player>();

        AwakeDate();
    }

    private void AwakeDate()
    {
        PlayerStat.year = PlayerPrefs.GetInt("Year");
        PlayerStat.month = PlayerPrefs.GetInt("Month");
        PlayerStat.day = PlayerPrefs.GetInt("Day");
        PlayerStat.hour = PlayerPrefs.GetInt("Hour");
        PlayerStat.minute = PlayerPrefs.GetInt("Minute");
    }

    private IEnumerator nowDate()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);

            PlayerStat.minute++;
            if (PlayerStat.minute >= 60)
            {
                PlayerStat.minute = 0;
                PlayerStat.hour++;

                if(PlayerStat.hour >= 24)
                {
                    PlayerStat.hour = 0;
                    PlayerStat.day++;

                    if(PlayerStat.day >= 31)
                    {

                    }
                }
            }
        }
    }
}

