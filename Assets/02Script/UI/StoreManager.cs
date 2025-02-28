using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    public static Action<ItemSO> OnSellItem; //아이템을 샀다

    [SerializeField] private GameObject sellCard; //판매 카드
    [Space (10f)]
    [SerializeField] private ItemSO[] allItemSOs; //아이템 모두

    private int sellCount = 10; //하루 팔 아이템 양
    [SerializeField] private ItemSO[] sellItem; //팔 아이템들

    private void Awake()
    {
        AddCard();
        DecideSellItem();
    }

    private void AddCard()
    {
        for (int i = 0; i < sellCount; i++)
        {
            GameObject card = Instantiate(sellCard, transform);
        }
    }

    private void DecideSellItem() //팔 아이템 정하기
    {
        sellItem = new ItemSO[sellCount];
        int maxNum = allItemSOs.Length - 1;
        int[] randomItmeNums = new int[sellCount];

        for(int i = 0; i < sellCount; i++)
        {
            int random = UnityEngine.Random.Range(0, maxNum);
            if(Array.Exists(randomItmeNums, x => x == random))
            {
                i--;
                continue;
            }
            else
            {
                randomItmeNums[i] = random;
                sellItem[i] = allItemSOs[random];
            }
        }
        ChildStore();
    }

    private void ChildStore()
    {
        StoreSell[] stores = GetComponentsInChildren<StoreSell>();

        for(int i = 0; i < sellCount; i++)
        {
            stores[i].ShowSet(sellItem[i]);
        }
    }
}
