using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemCard : MonoBehaviour
{
    [SerializeField] private ItemSO so; //������ ����

    [SerializeField] private GameObject itemPrefabs; //���� ������

    [SerializeField] private int countItme; //������ ���� ����

    private static ItemCard currentUseItem; //���� ������� ������
    private static bool useTrue; //ture : ������� ������ ����, false : ������� ������ ����

    private TextMeshProUGUI countText; //���� ���� �ؽ�Ʈ
    private Image cardImage; //������ �̹���


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
        HideItem();
        cardImage.color = Color.white;


        countItme = PlayerPrefs.GetInt(so.name);
        countText = GetComponentInChildren<TextMeshProUGUI>();
        ShowCount();

        Store.OnSellItem += GetItem;
    }

    public void SO(ItemSO itemSO)
    {
        so = itemSO;
    }

    public void ClickCard() //������ UI ��ư Ŭ�� ��
    {
        if (!useTrue) HoldItem();
        else if (useTrue && !so.isUse) { currentUseItem.HideItem(); HoldItem(); }
        else if (useTrue && so.isUse) HideItem();
        else return;
    }

    private void HoldItem() //������ Ȱ��ȭ
    {
        cardImage.color = new Color(95 / 225f, 95 / 225f, 95 / 225f, 1);
        so.isUse = true;
        useTrue = true;
        currentUseItem = this;

        itemPrefabs.transform.SetParent( GameManager.Instance.Player.transform,false); //�÷��̾�� �θ� ����
        itemPrefabs.SetActive(true);
    }

    public void HideItem() //������ ��Ȱ��ȭ
    {
        cardImage.color = Color.white;
        so.isUse = false;
        useTrue = false;
        currentUseItem = null;

        itemPrefabs.transform.SetParent(transform, false);
        itemPrefabs.SetActive(false);
    }

    public bool HaveItem() //�̹� ���� ������ ����
    {
        return so.getItem;
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
            PlayerPrefs.SetInt(so.name, --countItme);
            if (countItme < 1)
            {
                so.getItem = false;
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
            so.getItem = true;
            PlayerPrefs.SetInt(so.name, ++countItme);
            ShowCount();
        }
    }

    private void OnDisable()
    {
        Store.OnSellItem -= GetItem;
    }
}
