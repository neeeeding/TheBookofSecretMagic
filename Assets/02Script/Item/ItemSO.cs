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

public enum ItemCategory
{
    gift,
    potion,
    book,
    special,
    work,
    chat,
    hate,
    notbad,
    coin,
    mouse,
    none
}

public enum ItemType
{
    none, //����.

    lovePoition, //����
    staminaPoition,
    painPoition,

    blackbook, //å
    healbook,
    firebook,
    waterbook,
    copybook,
    potionbook,

    umbrella, //���
    broomstick, //���ڷ� (�����̵� ������)
    fan, //��ä
    hotPack, //����
    fryingPan, //�Ķ�����
    flower, //�ɴٹ� (����)
    foragingBin, //���� ä����

    drug, //����
    box, //����
    
    glasses, //�Ӹ����� ���� �Ȱ� (�ùٸ� ������)
    readingGlasses, //������ (��ȣ�ϴ� ������ 2��)
    emptyGlass, //�� ������

    worm, //���� ��
    trashGlass, //������ ������

    perfectGlass, //���°� ������

    coin, //����

    restyMouse, //Ŀ��
    chrisMouse, //Ŀ��
    theoMouse, //Ŀ��
    noahMouse, //Ŀ��
    niaMouse, //Ŀ��
    villainMouse, //Ŀ��
    harryMouse, //Ŀ��
    danielMouse, //Ŀ��
    pioMouse, //Ŀ��

    gift //������
}
