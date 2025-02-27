using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCardManager : MonoBehaviour
{
    private void Awake()
    {
        ItemCardSetActive();
        StoreManager.OnSellItem += ActionItemActive;
    }

    private void ActionItemActive(ItemSO so)
    {
        ItemCardSetActive();
    }

    private void ItemCardSetActive()
    {
        foreach (Transform card in gameObject.transform)
        {
            if (card.TryGetComponent(out ItemCard cardSc))
            {
                card.gameObject.SetActive(cardSc.HaveItem());
            }
        }
    }

    private void OnDisable()
    {
        StoreManager.OnSellItem -= ActionItemActive;
    }
}
