using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCardManager : MonoBehaviour
{
    [SerializeField] private ItemSO[] itemSOs; //��Ⱑ ������ ������
    [SerializeField] private GameObject itemCard; //������ ī��
    [SerializeField] private ItemHold item; //������

    private static List<ItemSO> items = new List<ItemSO>(); //��Ȱ��ȭ �ϴ� �� ������
    private bool isSetting; //true : ���� �Ϸ� ��, false : ������ �ȵ� (�ؾ� �ϴ�)

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
