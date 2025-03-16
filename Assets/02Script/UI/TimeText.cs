using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeText : MonoBehaviour
{
    private TextMeshProUGUI timeText;
    private PlayerStatSC stat;

    private void Start()
    {
        timeText = GetComponent<TextMeshProUGUI>();
        stat = GameManager.Instance.PlayerStat;
    }

    private void Update()
    {
        bool pm = stat.hour >= 12 && stat.hour != 24;
        timeText.text = $"{stat.month} / {stat.day}\n{(pm ? "����" : "����")} {(pm ? stat.hour -12 : stat.hour)} : {stat.minute} ";
    }
}
