using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "SO/ItmeSO")]
public class ItemSO : ScriptableObject
{
    [SerializeField]private string itemName; //������ �̸�

    public ItemCategory category; //ī�װ�
    public ItemType itemType; //������ ����

    public int sellCoin; //�Ĵ� ����

    public Sprite itemImage; //�����
}

public enum ItemCategory //ī�װ�
{
    gift = 8000,
    potion= 1000,
    book = 2000,
    special = 3000,
    work = 4000,
    chat = 5000,
    hate = 6000,
    notbad = 7000,
    coin = 9000,
    mouse = 10000,
    none = 0
}

public enum ItemType //����
{
    none = 0, //����.

    lovePotion = 1001, //����
    staminaPotion = 1002,
    painPotion = 1003,

    blackbook = 2001, //å
    healbook = 2002,
    firebook = 2003,
    waterbook = 2004,
    copybook = 2005,
    potionbook = 2006,

    umbrella = 3001, //���
    broomstick = 3002, //���ڷ� (�����̵� ������)
    fan = 3003, //��ä
    hotPack = 3004, //����
    fryingPan = 3005, //�Ķ�����
    flower = 3006, //�ɴٹ� (����)
    foragingBin = 3007, //���� ä����
    emptyGlass = 3008, //�� ������

    drug = 4001, //����
    box = 4002, //����
    
    glasses = 5001, //�Ӹ����� ���� �Ȱ� (�ùٸ� ������)
    readingGlasses = 5002, //������ (��ȣ�ϴ� ������ 2��)

    worm = 6001, //���� ��
    trashGlass = 6002, //������ ������

    perfectGlass = 7001, //���°� ������

    coin = 9001, //����

    restyMouse = 10001, //Ŀ��
    chrisMouse = 10002,
    pioMouse = 10003,
    theoMouse = 10004, 
    noahMouse = 10005,
    niaMouse = 10006,
    villainMouse = 10007,
    harryMouse = 10008, 
    danielMouse = 10009,

    gift = 8000 //������
}
