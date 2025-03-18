using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCardManager : MonoBehaviour
{
    [SerializeField] private ItemSO[] itemSOs; //들기가 가능한 아이템
    [SerializeField] private GameObject itemCard; //아이템 카드
    [SerializeField] private ItemHold item; //아이템

    private static List<ItemSO> items = new List<ItemSO>(); //비활성화 하는 애 때문에
    private bool isSetting; //true : 세팅 완료 된, false : 세팅이 안된 (해야 하는)

    private void Awake()
    {
        isSetting = false;
        GameManager.OnStart += GameStart;
    }

    private void OnEnable()
    {
        LoadCard.OnLoad += LoadItem;
        Store.OnSellItem += GetItem;
        if(GameManager.Instance.isStart)
        {
            if (!isSetting)
            {
                AddAllItems();
            }
            LoadItem();
        }
    }

    private void GameStart()
    {
        AddAllItems();
    }

    private void AddAllItems()
    {
        isSetting = true;
        for(int i = 0; i < itemSOs.Length; i++)
        {
            items.Add(itemSOs[i]);
            GameObject card = Instantiate(itemCard, transform);
            ItemCard cardSc = card.GetComponent<ItemCard>();
            cardSc.SetCard(items[i], item);
        }
    }

    private void LoadItem()
    {
        for(int i = 0; i< itemSOs.Length; i++)
        {
            ItemSO so = items[i];
            ActionItemActive(so,true);

        }
    }

    private void GetItem(ItemSO so)
    {
        ActionItemActive(so, false);
    }

    private void ActionItemActive(ItemSO so, bool b)
    {
        if(so.category != ItemCategory.mouse && so.category != ItemCategory.coin)
        {
            foreach (Transform card in gameObject.transform)
            {
                if (card.TryGetComponent(out ItemCard cardSc))
                {
                    card.gameObject.SetActive(cardSc.HaveItem(so,b));
                }
            }
        }
    }

    private void OnDisable()
    {
        GameManager.OnStart -= GameStart;
        Store.OnSellItem -= GetItem;
        LoadCard.OnLoad -= LoadItem;
    }
}
