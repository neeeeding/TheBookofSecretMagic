using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCardManager : MonoBehaviour
{
    [SerializeField] private ItemSO[] itemSOs; //들기가 가능한 아이템
    [SerializeField] private GameObject itemCard; //아이템 카드
    [SerializeField] private ItemHold item; //아이템

    private static List<ItemSO> items; //비활성화 하는 애 때문에

    private void Awake()
    {
        GameManager.OnStart += GameStart;
    }

    private void OnEnable()
    {
        LoadCard.OnLoad += LoadItem;
        Store.OnSellItem += GetItem;
    }

    private void GameStart()
    {
        AddAllItems();
    }

    private void AddAllItems()
    {
        for(int i = 0; i < itemSOs.Length; i++)
        {
            items.Add(itemSOs[i]);
            GameObject card = Instantiate(itemCard, transform);
            ItemCard cardSc = card.GetComponent<ItemCard>();
            cardSc.SetCard(itemSOs[i], item);
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
