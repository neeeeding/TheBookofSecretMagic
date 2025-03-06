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
        if (so.category == ItemCategory.coin)
        {
            GameManager.Instance.AddCoin(+so.sellCoin);

            Store.OnSellItem?.Invoke(so);
            GameManager.CoinText?.Invoke();
        }
        else
        {
            if (GameManager.Instance.PlayerStat.playerCoin <= so.sellCoin)
            {
                GameManager.Instance.AddCoin(-so.sellCoin);

                so.getItem = true;

                Store.OnSellItem?.Invoke(so);
                GameManager.CoinText?.Invoke();
            }
            else
            {
                //소지금 부족 안내
            }
        }
    }
}
