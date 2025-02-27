using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreSell : MonoBehaviour
{
    private ItemSO so; //아이템
    [SerializeField] private Image itmeImgae; //생긴거
    private TextMeshProUGUI coinText; //제시 가격

    private void Awake()
    {
        coinText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void ShowSet(ItemSO itemso)
    {
        so = itemso;
        itmeImgae.sprite = so.itemImage;
        coinText.text = so.sellCoin.ToString();
    }
}
