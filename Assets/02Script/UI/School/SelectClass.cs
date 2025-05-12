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
