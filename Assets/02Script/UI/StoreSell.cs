using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreSell : MonoBehaviour
{
    [SerializeField] private ItemSO so; //������
    private Image itmeImgae; //�����
    private TextMeshProUGUI coinText; //���� ����

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
            //������ ���� �ȳ�
        }
    }
}
