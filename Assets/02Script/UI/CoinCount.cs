using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CoinCount : MonoBehaviour
{
    private TextMeshProUGUI coinText;

    private void Awake()
    {
        coinText = GetComponentInChildren<TextMeshProUGUI>();
        Text();
        GameManager.OnStart += GameStart;
    }

    private void GameStart()
    {
        GameManager.CoinText += Text;
    }

    private void Text()
    {
        coinText.text = GameManager.Instance.PlayerStat.playerCoin.ToString();
    }

    private void OnDisable()
    {
        GameManager.OnStart -= GameStart;
        GameManager.CoinText -= Text;
    }
}
