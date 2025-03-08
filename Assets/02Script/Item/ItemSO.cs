using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "SO/ItmeSO")]
public class ItemSO : ScriptableObject
{
    [SerializeField]private string itemName; //아이템 이름

    public ItemCategory category; //카테고리
    public ItemType itemType; //아이템 종류

    public int sellCoin; //파는 가격

    public Sprite itemImage; //생긴거
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
    none, //없다.

    lovePoition, //포션
    staminaPoition,
    painPoition,

    blackbook, //책
    healbook,
    firebook,
    waterbook,
    copybook,
    potionbook,

    umbrella, //우산
    broomstick, //빗자루 (순간이동 아이템)
    fan, //부채
    hotPack, //핫팩
    fryingPan, //후라이팬
    flower, //꽃다발 (연애)
    foragingBin, //곤충 채집통

    drug, //마약
    box, //상자
    
    glasses, //속마음을 보는 안경 (올바른 선택지)
    readingGlasses, //돋보기 (선호하는 아이템 2개)
    emptyGlass, //빈 유리병

    worm, //벌레 들
    trashGlass, //실패한 유리병

    perfectGlass, //생태계 유리병

    coin, //코인

    restyMouse, //커서
    chrisMouse, //커서
    theoMouse, //커서
    noahMouse, //커서
    niaMouse, //커서
    villainMouse, //커서
    harryMouse, //커서
    danielMouse, //커서
    pioMouse, //커서

    gift //아직임
}
