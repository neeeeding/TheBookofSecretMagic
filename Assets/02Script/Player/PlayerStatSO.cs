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
    [Range(0, 100)]
    public int resty;//레스티
    [Range(0, 100)]
    public int chris; //크리스
    [Range(0, 100)]
    public int theo; //테오
    [Range(0, 100)]
    public int pio; //피오
    [Range(0, 100)]
    public int noah; //노아
    [Range(0, 100)]
    public int nia; //니아
    [Range(0, 100)]
    public int villain; //빌런
    [Range(0, 100)]
    public int harry; //해리
    [Range(0, 100)]
    public int daniel; //다니엘

    [Space(50f)]
    public string lastText; //마지막 대화

    [Space(50f)]
    public int year;
    public int month;
    public int day;
    public int hour;
    public int minute;

    public ItemSO[] allItem;

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

        resty = 0;
        chris = 0;
        theo = 0;
        pio = 0;
        noah = 0;
        nia = 0;
        villain = 0;
        harry = 0;
        daniel = 0;

        lastText = "마지막 대화가 없습니다.";

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
