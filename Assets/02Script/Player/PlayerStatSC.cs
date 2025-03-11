using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatSC
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



    public int lovePoitionCount; //포션
    public int staminaPoitionCount;
    public int painPoitionCount;

    public int blackbookCount; //책
    public int healbookCount;
    public int firebookCount;
    public int waterbookCount;
    public int copybookCount;
    public int potionbookCount;

    public int umbrellaCount; //우산
    public int broomstickCount; //빗자루 (순간이동 아이템)
    public int fanCount; //부채
    public int hotPackCount; //핫팩
    public int fryingPanCount; //후라이팬
    public int flowerCount; //꽃다발 (연애)
    public int foragingBinCount; //곤충 채집통

    public int drugCount; //마약
    public int boxCount; //상자

    public int glassesCount; //속마음을 보는 안경 (올바른 선택지)
    public int readingGlassesCount; //돋보기 (선호하는 아이템 2개)
    public int emptyGlassCount; //빈 유리병

    public int wormCount; //벌레 들
    public int trashGlassCount; //실패한 유리병

    public int perfectGlassCount; //생태계 유리병

    public int restyMouse; //커서
    public int chrisMouse; //커서
    public int theoMouse; //커서
    public int noahMouse; //커서
    public int niaMouse; //커서
    public int villainMouse; //커서
    public int harryMouse; //커서
    public int danielMouse; //커서
    public int pioMouse; //커서

    public int giftCount; //아직임

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
