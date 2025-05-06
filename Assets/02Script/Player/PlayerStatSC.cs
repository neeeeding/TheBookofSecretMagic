using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStatSC
{
    public bool isChat; //ture : ä�� ��, false : �� ����

    [Range(0,100)] //�ɷ�ġ
    public float blackMagic;
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

    public Vector2 playerPosition; //�÷��̾� ��ġ

    public PlayerJob job; //����

    [Space(50f)]
    public Character lastCharacter; //������ ĳ����
    public CharacterSO lastSO;
    public string lastText; //������ ��ȭ

    public SaveDictionary<CharacterName, SaveDictionary<DialogType, string>> characterlastText; //ĳ���� ������ ��ȭ �̸�<���̾�α�(����), ��°(Ȥ�� �ؽ�Ʈ)>

    #region ���� ȣ���� ���� ��
    //[Space(50f)] //ȣ����
    //[Range(0, 100)]
    //public int resty;//����Ƽ
    //[Range(0, 100)]
    //public int chris; //ũ����
    //[Range(0, 100)]
    //public int theo; //�׿�
    //[Range(0, 100)]
    //public int pio; //�ǿ�
    //[Range(0, 100)]
    //public int noah; //���
    //[Range(0, 100)]
    //public int nia; //�Ͼ�
    //[Range(0, 100)]
    //public int villain; //����
    //[Range(0, 100)]
    //public int harry; //�ظ�
    //[Range(0, 100)]
    //public int daniel; //�ٴϿ�
    #endregion

    #region ���� ���� ���� ��
    //public int[] restyLastText = new int[2]; // ����Ƽ
    //public int[] chrisLastText = new int[2]; // ũ����
    //public int[] theoLastText = new int[2]; // �׿�
    //public int[] pioLastText = new int[2]; // �ǿ�
    //public int[] noahLastText = new int[2]; // ���
    //public int[] niaLastText = new int[2]; // �Ͼ�
    //public int[] villainLastText = new int[2]; // ����
    //public int[] harryLastText = new int[2]; // �ظ�
    //public int[] danielLastText = new int[2]; // �ٴϿ�
    #endregion

    [Space(50f)] //��¥
    public int year;
    public int month;
    public int day;
    public int hour;
    public int minute;

    public SaveDictionary<ItemCategory, SaveDictionary<ItemType, int>> items; //�����۵� ī�װ�<����,��>
    

    #region ���� ������ ���� ��
    //public int lovePotionCount; //����
    //public int staminaPotionCount;
    //public int painPotionCount;

    //public int blackbookCount; //å
    //public int healbookCount;
    //public int firebookCount;
    //public int waterbookCount;
    //public int copybookCount;
    //public int potionbookCount;

    //public int umbrellaCount; //���
    //public int broomstickCount; //���ڷ� (�����̵� ������)
    //public int fanCount; //��ä
    //public int hotPackCount; //����
    //public int fryingPanCount; //�Ķ�����
    //public int flowerCount; //�ɴٹ� (����)
    //public int foragingBinCount; //���� ä����

    //public int drugCount; //����
    //public int boxCount; //����

    //public int glassesCount; //�Ӹ����� ���� �Ȱ� (�ùٸ� ������)
    //public int readingGlassesCount; //������ (��ȣ�ϴ� ������ 2��)
    //public int emptyGlassCount; //�� ������

    //public int wormCount; //���� ��
    //public int trashGlassCount; //������ ������

    //public int perfectGlassCount; //���°� ������

    //public int restyMouse; //Ŀ��
    //public int chrisMouse; //Ŀ��
    //public int theoMouse; //Ŀ��
    //public int noahMouse; //Ŀ��
    //public int niaMouse; //Ŀ��
    //public int villainMouse; //Ŀ��
    //public int harryMouse; //Ŀ��
    //public int danielMouse; //Ŀ��
    //public int pioMouse; //Ŀ��

    //public int giftCount; //������
    #endregion

    [ContextMenu("ResetStat")]
    public void ResetStat()
    {
        blackMagic = 0;
        healMagic = 0;
        fireMagic = 0;
        waterMagic = 0;
        copyMagic = 0;
        potionMagic = 0;

        playerCoin = 5;

        job = PlayerJob.none;

        //resty = 0;
        //chris = 0;
        //theo = 0;
        //pio = 0;
        //noah = 0;
        //nia = 0;
        //villain = 0;
        //harry = 0;
        //daniel = 0;

        lastText = "������ ��ȭ�� �����ϴ�.";

        year = 2000;
        month = 1;
        day = 1;
        hour = 1;
        minute = 0;
    }
}
