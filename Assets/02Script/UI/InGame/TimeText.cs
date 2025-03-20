using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimeText : MonoBehaviour
{
    private TextMeshProUGUI timeText;
    private static PlayerStatSC stat;

    private void Awake()
    {
        GameManager.OnStart += GameStart;
        timeText = GetComponent<TextMeshProUGUI>();
    }

    private void GameStart()
    {
        stat = GameManager.Instance.PlayerStat;
    }

    private void OnEnable()
    {
        LoadCard.OnLoad += ChangeStat;
    }

    private void ChangeStat()
    {
        stat = GameManager.Instance.PlayerStat;
    }

    private void Update()
    {
        bool pm = stat.hour >= 12 && stat.hour != 24;
        timeText.text = $"{stat.month} / {stat.day}\n{(pm ? "오후" : "오전")} {(pm ? stat.hour -12 : stat.hour)} : {stat.minute} ";
    }

    private void OnDisable()
    {
        GameManager.OnStart -= GameStart;
        LoadCard.OnLoad -= ChangeStat;
    }
}
