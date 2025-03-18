using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemCard : MonoBehaviour
{
    [SerializeField] private ItemSO so; //������ ����

    [SerializeField] private int countItme; //������ ���� ����

    private static ItemCard currentUseItem; //���� ������� ������
    private static bool useTrue; //ture : ������� ������ ����, false : ������� ������ ����

    private TextMeshProUGUI countText; //���� ���� �ؽ�Ʈ
    private Image cardImage; //������ �̹���

    private ItemHold realItem;

    private int itemCount; //������ ����
    private bool getItem; //�������� �������.
    private bool isUse; //��� �ִ���

    [ContextMenu("ResetCount")]
    public void ResetCount()
    {
        countItme = 0;
        PlayerPrefs.SetInt($"{so.name}", 0);
        PlayerPrefs.Save();
    }

    private void Awake()
    {
        cardImage = GetComponent<Image>();
        //cardImage.sprite = so.itemImage;
        PlayerPrefs.SetInt(so.name, 1);


        //countItme = PlayerPrefs.GetInt(so.name);
        countText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        LoadCard.OnLoad += HideItem;
    }

    public void SetCard(ItemSO mySO, ItemHold item) //ī�� ���� �����ֱ� (���� �ε�)
    {
        so = mySO;
        realItem = item;
        HideItem();

        getItem = GameManager.Instance.Items[mySO.itemType] <= 0 ? false : true;
        countItme = GameManager.Instance.Items[mySO.itemType];
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
        realItem.so = so;
    }

    public void HideItem() //������ ��Ȱ��ȭ
    {
        cardImage.color = Color.white;
        isUse = false;
        useTrue = false;
        currentUseItem = null;

        realItem.gameObject.SetActive(false);
        realItem.so = null;
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

    private void UseItme(ItemSO currentSO) //�������� �����
    {
        if(currentSO == so)
        {
            GameManager.Instance.AddItemCount(so.category, so.itemType, -1);
            countItme = GameManager.Instance.Items[currentSO.itemType];
            //PlayerPrefs.SetInt(so.name, --countItme);
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
            countItme = GameManager.Instance.Items[currentSO.itemType];
            getItem = true;
            //PlayerPrefs.SetInt(so.name, ++countItme);
            ShowCount();
        }
    }

    private void OnDisable()
    {
        LoadCard.OnLoad -= HideCard;
    }
}
