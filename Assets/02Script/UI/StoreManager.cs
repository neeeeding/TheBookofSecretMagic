using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    public static Action<ItemSO> OnSellItem; //�������� ���

    [SerializeField] private GameObject sellCard; //�Ǹ� ī��
    [Space (10f)]
    [SerializeField] private ItemSO[] allItemSOs; //������ ���

    private int sellCount = 10; //�Ϸ� �� ������ ��
    [SerializeField] private ItemSO[] sellItem; //�� �����۵�

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

    private void DecideSellItem() //�� ������ ���ϱ�
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
