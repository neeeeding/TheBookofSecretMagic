using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCardManager : MonoBehaviour
{
    [SerializeField] private ItemSO[] itemSOs; //들기가 가능한 아이템
    [SerializeField] private GameObject itemCard; //아이템 카드
    [SerializeField] private ItemHold item; //아이템
    private void Awake()
    {
        Store.OnSellItem += GetItem;
        LoadCard.OnLoad += LoadItem;
        AddAllItems();
    }

    private void AddAllItems()
    {
        for(int i = 0; i < itemSOs.Length; i++)
        {
            GameObject card = Instantiate(itemCard, transform);
            ItemCard cardSc = card.GetComponent<ItemCard>();
            cardSc.SetCard(itemSOs[i], item);
        }
    }

    private void LoadItem()
    {
        for(int i = 0; i< itemSOs.Length; i++)
        {
            ItemSO so = itemSOs[i];
            ActionItemActive(so,false);

        }
    }

    private void GetItem(ItemSO so)
    {

    }

    private void ActionItemActive(ItemSO so, bool b)
    {
        if(so.category != ItemCategory.mouse && so.category != ItemCategory.coin)
        {
            foreach (Transform card in gameObject.transform)
            {
                if (card.TryGetComponent(out ItemCard cardSc))
                {
                    card.gameObject.SetActive(cardSc.HaveItem(so));
                }
            }
        }
    }

    private void OnDisable()
    {
        Store.OnSellItem -= GetItem;
        LoadCard.OnLoad -= LoadItem;
    }
}
