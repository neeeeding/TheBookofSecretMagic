using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class SelectClass : MonoBehaviour
{
    [Header("Need")]
    [SerializeField] private TMP_Dropdown[] select; // �ش� ��� �ٿ�

    private void Awake()
    {
        foreach (TMP_Dropdown item in select)
        {
            SetClass(item);
        }

    }

    //���� �� ���� �ȵǵ��� ����
    public void CompleteBtn() //�Ϸ� ��ư ������ ��
    {
        SchoolManager.Instance.ClearToday(); //Ȥ�� �𸣴� ����ֱ�

        for(int i = 1; i <=select.Length; i++)
        {
            print(i);
            TMP_Dropdown item = select[i-1];

            string value = item.options[item.value].text;
            PlayerJob v = value switch //�����ϴ� ���ڸ� ���� ��ȯ
            {
                string s when s.Contains("��") => PlayerJob.brack,
                string s when s.Contains("ġ��") => PlayerJob.heal,
                string s when s.Contains("��") => PlayerJob.fire,
                string s when s.Contains("��") => PlayerJob.water,
                string s when s.Contains("����") => PlayerJob.copy,
                string s when s.Contains("����") => PlayerJob.potion,
                _ => PlayerJob.none
            };

            SchoolManager.Instance.todayClass.Add(i,v ); //�߰��ϱ�
        }
        SchoolManager.Instance.SettingTodayClass(); //���� ������ ���� �˷��ֱ�
        UISettingManager.Instance.InGame();
    }

    private void SetClass(TMP_Dropdown dropdown) //�ش� ��� �ٿ �ʿ��� ���� �־��ֱ�.
    {
        dropdown.options.Clear(); //����ֱ�
        foreach(PlayerJob item in Enum.GetValues(typeof(PlayerJob)))
        {
            TMP_Dropdown.OptionData data = new TMP_Dropdown.OptionData(); //�ϳ� ����

            if(item == PlayerJob.none)
            {
                data.text = $"������ �����ϼ���.";
            }
            else if(item == PlayerJob.potion)
            {
                data.text = $"{ChatSetting.Name<PlayerJob>(item)} ���� ����";
            }
            else
            {
                data.text = $"{ChatSetting.Name<PlayerJob>(item)} ���� ����";
            }
            dropdown.options.Add(data);
        }
    }
}
