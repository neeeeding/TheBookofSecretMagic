using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreSell : MonoBehaviour
{
    [SerializeField] private ItemSO so; //아이템
    private Image itmeImgae; //생긴거
    private TextMeshProUGUI coinText; //제시 가격

    private void Awake()
    {
        coinText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void ShowSet(ItemSO itemso)
    {
        so = itemso;
        //itmeImgae.sprite = so.itemImage;
        coinText.text = so.sellCoin.ToString();
    }

    public void ClickSell()
    {
        if (GameManager.Instance.PlayerStat.playerCoin <= so.sellCoin)
        {
            GameManager.Instance.PlayerStat.playerCoin -= so.sellCoin;

            so.getItem = true;

            StoreManager.OnSellItem?.Invoke(so);
        }
        else
        {
            //소지금 부족 안내
        }
    }
}
