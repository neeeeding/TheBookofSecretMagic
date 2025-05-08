using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ItemCard : MonoBehaviour
{
    public static Action<ItemSO> OnHoldItem; //��� �ִ� ������ �����ֱ�

    [SerializeField] private ItemSO so; //������ ����

    [SerializeField] private int countItme; //������ ���� ����

    private bool getItem; //�������� �������.

    private static ItemCard currentUseItem; //���� ������� ������
    private static bool useTrue; //ture : ������� ������ ����, false : ������� ������ ����
    private bool isUse; //��� �ִ���
    private ItemHold realItem; //�鸮�� �� ������(��ġ)

    private TextMeshProUGUI countText; //���� ���� �ؽ�Ʈ
    private Image cardImage; //������ �̹���

    private void Awake()
    {
        cardImage = GetComponent<Image>();
        //cardImage.sprite = so.itemImage;

        countText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        LoadCard.OnLoad += HideItem;
        Dialog.OnGame += HideItem;
    }

    public void SetCard(ItemSO mySO, ItemHold item) //ī�� ���� �����ֱ� (���� �ε�)
    {
        so = mySO;
        realItem = item;
        HideItem();

        countItme = GameManager.Instance.PlayerStat.items[mySO.category][mySO.itemType]; //���� �ֱ�
        getItem = countItme <= 0 ? false : true; // 0 �̸�����
        ShowCount();
        if (!getItem)
        {
            HideCard();
        }
    }

    public void ClickCard() //������ UI ��ư Ŭ�� ��
    {
        if (!useTrue) HoldItem();
        else if (useTrue && !isUse) { currentUseItem.HideItem(); HoldItem(); }
        else if (useTrue && isUse) HideItem();
        else return;
    }

    private void HoldItem() //������ Ȱ��ȭ
    {
        cardImage.color = new Color(95 / 225f, 95 / 225f, 95 / 225f, 1);
        isUse = true;
        useTrue = true;
        currentUseItem = this;

        realItem.gameObject.SetActive(true);
        realItem.Setting(so, this);
        OnHoldItem?.Invoke(so);
    }

    public void HideItem() //������ ��Ȱ��ȭ
    {
        cardImage.color = Color.white;
        isUse = false;
        useTrue = false;
        currentUseItem = null;

        realItem.gameObject.SetActive(false);
        realItem.Setting(null,null);
        OnHoldItem?.Invoke(null);
    }

    public bool HaveItem(ItemSO currentSO, bool b) //�̹� ���� ������ ����
    {
        GetItem(currentSO);
        if(b)
            UseItme(currentSO);
        return getItem;
    }

    private void HideCard() //ī�� �����
    {
        gameObject.SetActive(false);
    }

    private void ShowCount() //������ ���� �� �ؽ�Ʈ
    {
        if(countItme > 1)
        {
            countText.gameObject.SetActive(true);
            countText.text = countItme.ToString();
        }
        else
        {
            countText.gameObject.SetActive(false);
        }
    }

    private void UseItme(ItemSO currentSO) //�������� ����� (����)
    {
        if(currentSO == so)
        {
            GameManager.Instance.AddItemCount(so.category, so.itemType, -1);
            countItme = GameManager.Instance.PlayerStat.items[currentSO.category][currentSO.itemType];

            if (countItme < 1)
            {
                getItem = false;
                HideCard();
                return;
            }
            ShowCount();
        }
    }

    private void GetItem(ItemSO currentSO) //�������� ����
    {
        if(currentSO == so)
        {
            GameManager.Instance.AddItemCount(so.category, so.itemType, 1);
            countItme = GameManager.Instance.PlayerStat.items[currentSO.category][currentSO.itemType];
            getItem = true;

            ShowCount();
        }
    }

    private void OnDisable()
    {
        LoadCard.OnLoad -= HideItem;
        Dialog.OnGame -= HideItem;
    }
}
