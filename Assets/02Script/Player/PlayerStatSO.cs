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
