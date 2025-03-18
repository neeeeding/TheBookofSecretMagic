using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemCard : MonoBehaviour
{
    [SerializeField] private ItemSO so; //아이템 정보

    [SerializeField] private int countItme; //아이템 소지 개수

    private static ItemCard currentUseItem; //현재 사용중인 아이템
    private static bool useTrue; //ture : 사용중인 아이템 있음, false : 사용중인 아이템 없음

    private TextMeshProUGUI countText; //소지 개수 텍스트
    private Image cardImage; //아이템 이미지

    private ItemHold realItem;

    private int itemCount; //아이템 개수
    private bool getItem; //아이템을 얻었는지.
    private bool isUse; //들고 있는중

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

    public void SetCard(ItemSO mySO, ItemHold item) //카드 정보 정해주기 (세팅 로드)
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

    public void ClickCard() //아이템 UI 버튼 클릭 시
    {
        if (!useTrue) HoldItem();
        else if (useTrue && !isUse) { currentUseItem.HideItem(); HoldItem(); }
        else if (useTrue && isUse) HideItem();
        else return;
    }

    private void HoldItem() //아이템 활성화
    {
        cardImage.color = new Color(95 / 225f, 95 / 225f, 95 / 225f, 1);
        isUse = true;
        useTrue = true;
        currentUseItem = this;

        realItem.gameObject.SetActive(true);
        realItem.so = so;
    }

    public void HideItem() //아이템 비활성화
    {
        cardImage.color = Color.white;
        isUse = false;
        useTrue = false;
        currentUseItem = null;

        realItem.gameObject.SetActive(false);
        realItem.so = null;
    }

    public bool HaveItem(ItemSO currentSO, bool b) //이미 얻은 아이템 인지
    {
        GetItem(currentSO);
        if(b)
            UseItme(currentSO);
        return getItem;
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

    private void GetItem(ItemSO currentSO) //아이템을 얻음
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
