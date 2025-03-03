using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatSO", menuName = "SO/PlayerStatSO")]

public class PlayerStatSO : ScriptableObject
{
    [Range(0,100)]
    public float backMagic;
    [Range(0, 100)]
    public float healMagic;
    [Range(0, 100)]
    public float fireMagic;
    [Range(0, 100)]
    public float waterMagic;
    [Range(0, 100)]
    public float copyMagic;
    [Range(0, 100)]
    public float potionMagic;

    public int playerCoin; //소지금

    public float playerSpeed; //걷는(뛰는 속도)

    public PlayerJob job; //전공

    [Space(50f)]
    public int year;
    public int month;
    public int day;
    public int hour;
    public int minute;

    [ContextMenu("ResetStat")]
    public void ResetStat()
    {
        backMagic = 0;
        healMagic = 0;
        fireMagic = 0;
        waterMagic = 0;
        copyMagic = 0;
        potionMagic = 0;

        playerCoin = 5;

        job = PlayerJob.none;

        year = 2000;
        month = 1;
        day = 1;
        hour = 1;
        minute = 0;
    }
}

public enum PlayerJob
{
    back, heal, fire, water, copy, potion, none
}
