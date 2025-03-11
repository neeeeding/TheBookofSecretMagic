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

    public int playerCoin; //������

    public float playerSpeed; //�ȴ�(�ٴ� �ӵ�)

    public PlayerJob job; //����

    [Space(50f)]
    [Range(0, 100)]
    public int resty;//����Ƽ
    [Range(0, 100)]
    public int chris; //ũ����
    [Range(0, 100)]
    public int theo; //�׿�
    [Range(0, 100)]
    public int pio; //�ǿ�
    [Range(0, 100)]
    public int noah; //���
    [Range(0, 100)]
    public int nia; //�Ͼ�
    [Range(0, 100)]
    public int villain; //����
    [Range(0, 100)]
    public int harry; //�ظ�
    [Range(0, 100)]
    public int daniel; //�ٴϿ�

    [Space(50f)]
    public string lastText; //������ ��ȭ

    [Space(50f)]
    public int year;
    public int month;
    public int day;
    public int hour;
    public int minute;



    public int lovePoitionCount; //����
    public int staminaPoitionCount;
    public int painPoitionCount;

    public int blackbookCount; //å
    public int healbookCount;
    public int firebookCount;
    public int waterbookCount;
    public int copybookCount;
    public int potionbookCount;

    public int umbrellaCount; //���
    public int broomstickCount; //���ڷ� (�����̵� ������)
    public int fanCount; //��ä
    public int hotPackCount; //����
    public int fryingPanCount; //�Ķ�����
    public int flowerCount; //�ɴٹ� (����)
    public int foragingBinCount; //���� ä����

    public int drugCount; //����
    public int boxCount; //����

    public int glassesCount; //�Ӹ����� ���� �Ȱ� (�ùٸ� ������)
    public int readingGlassesCount; //������ (��ȣ�ϴ� ������ 2��)
    public int emptyGlassCount; //�� ������

    public int wormCount; //���� ��
    public int trashGlassCount; //������ ������

    public int perfectGlassCount; //���°� ������

    public int restyMouse; //Ŀ��
    public int chrisMouse; //Ŀ��
    public int theoMouse; //Ŀ��
    public int noahMouse; //Ŀ��
    public int niaMouse; //Ŀ��
    public int villainMouse; //Ŀ��
    public int harryMouse; //Ŀ��
    public int danielMouse; //Ŀ��
    public int pioMouse; //Ŀ��

    public int giftCount; //������

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

        lastText = "������ ��ȭ�� �����ϴ�.";

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
