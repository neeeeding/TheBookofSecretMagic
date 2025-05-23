using UnityEngine;
using TMPro;

public class TimeText : MonoBehaviour
{
    private TextMeshProUGUI timeText;
    private static PlayerStatSC stat;

    private void Awake()
    {
        GameManager.OnStart += ChangeStat;
        timeText = GetComponent<TextMeshProUGUI>();
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
        if (stat == null)
        {
            ChangeStat();
        }
        else
        {
            bool pm = stat.hour >= 12 && stat.hour != 24;
            timeText.text = $"{stat.month} / {stat.day}\n{(pm ? "����" : "����")} {(pm ? stat.hour - 12 : stat.hour)} : {stat.minute} ";
        }
    }

    private void OnDisable()
    {
        GameManager.OnStart -= ChangeStat;
        LoadCard.OnLoad -= ChangeStat;
    }
}
