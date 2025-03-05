using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemCard : MonoBehaviour
{
    [SerializeField] private ItemSO so; //아이템 정보

    [SerializeField] private GameObject itemPrefabs; //실제 아이템

    [SerializeField] private int countItme; //아이템 소지 개수

    private static ItemCard currentUseItem; //현재 사용중인 아이템
    private static bool useTrue; //ture : 사용중인 아이템 있음, false : 사용중인 아이템 없음

    private TextMeshProUGUI countText; //소지 개수 텍스트
    private Image cardImage; //아이템 이미지


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

    public void ClickCard() //아이템 UI 버튼 클릭 시
    {
        if (!useTrue) HoldItem();
        else if (useTrue && !so.isUse) { currentUseItem.HideItem(); HoldItem(); }
        else if (useTrue && so.isUse) HideItem();
        else return;
    }

    private void HoldItem() //아이템 활성화
    {
        cardImage.color = new Color(95 / 225f, 95 / 225f, 95 / 225f, 1);
        so.isUse = true;
        useTrue = true;
        currentUseItem = this;

        itemPrefabs.transform.SetParent( GameManager.Instance.Player.transform,false); //플레이어로 부모 변경
        itemPrefabs.SetActive(true);
    }

    public void HideItem() //아이템 비활성화
    {
        cardImage.color = Color.white;
        so.isUse = false;
        useTrue = false;
        currentUseItem = null;

        itemPrefabs.transform.SetParent(transform, false);
        itemPrefabs.SetActive(false);
    }

    public bool HaveItem() //이미 얻은 아이템 인지
    {
        return so.getItem;
    }

    private void HideCard() //카드 숨기기
    {
        gameObject.SetActive(false);
    }

    private void ShowCount() //아이템 소지 수 텍스트
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

    private void UseItme(ItemSO currentSO) //아이템을 사용함
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

    private void GetItem(ItemSO currentSO) //아이템을 얻음
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
