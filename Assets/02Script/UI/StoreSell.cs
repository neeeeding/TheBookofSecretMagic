using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreSell : MonoBehaviour
{
    private ItemSO so; //������
    [SerializeField] private Image itmeImgae; //�����
    private TextMeshProUGUI coinText; //���� ����

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
