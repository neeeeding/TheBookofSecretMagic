using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using System;

public class LoadCard : MonoBehaviour
{
    public static Action OnLoad;

    private string fileName;
    private string last;
    private string date;

    [SerializeField] private TextMeshProUGUI fileNameText;
    [SerializeField] private TextMeshProUGUI lastText;
    [SerializeField] private TextMeshProUGUI dateText;

    private void Awake()
    {
        if (fileName != "")
        {
            CardSetting();
        }
    }

    public void AwakeLoadSave(string name, PlayerStatSC saveStat) //���� �ٽ� �ε��ϱ�
    {
        fileName = name;
        Setting(saveStat);
    }

    public void ClcikSave(string name) //���� �̸����� �ۼ��� ���¿��� �Ϸ� ���� ��
    {
        fileName = name;

        Setting(GameManager.Instance.PlayerStat);

        string data = JsonUtility.ToJson(GameManager.Instance.PlayerStat);

        File.WriteAllText($"{Save.path}/{fileName}", data);
    }

    public void ClickLoad() //�ҷ����� ���� ��
    {
        string data = File.ReadAllText($"{Save.path}/{fileName}");
        PlayerStatSC stat = JsonUtility.FromJson<PlayerStatSC>(data);
        GameManager.Instance.PlayerStat = stat;
        OnLoad?.Invoke();
        GameManager.CoinText?.Invoke();
    }

    private void CardSetting() //ī�� ����
    {
        fileNameText.text = fileName;
        lastText.text = last;
        dateText.text = date;
    }

    private void Setting(PlayerStatSC saveStat ) //÷�� ����
    {
        PlayerStatSC stat = saveStat;

        last = stat.lastText;

        bool pm = stat.hour >= 12 && stat.hour != 24;

        date = $"{stat.month} / {stat.day}\n{(pm ? "����" : "����")} {(pm ? stat.hour - 12 : stat.hour)} : {stat.minute}";

        CardSetting();
    }

    public void DeleteMe() //���� ����
    {
        string[] load = File.ReadAllLines($"{Save.path}/saveName");
        File.Delete($"{Save.path}/saveName");
        
        for(int i = 0; i < load.Length; i++)
        {
            if(load[i] != fileName)
            {
                string name = load[i];
                File.AppendAllText($"{Save.path}/SaveName", $"{name}\n");
            }
        }

        File.Delete($"{Save.path}/{fileName}");

        Destroy(gameObject);
    }
}
