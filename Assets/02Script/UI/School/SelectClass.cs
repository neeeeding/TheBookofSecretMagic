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
    [SerializeField] private TMP_Dropdown[] select; // 해당 드롭 다운

    private void Awake()
    {
        foreach (TMP_Dropdown item in select)
        {
            SetClass(item);
        }
    }

    private void SetClass(TMP_Dropdown dropdown) //해당 드롭 다운에 필요한 내용 넣어주기.
    {
        dropdown.options.Clear(); //비어주기
        foreach(PlayerJob item in Enum.GetValues(typeof(PlayerJob)))
        {
            TMP_Dropdown.OptionData data = new TMP_Dropdown.OptionData(); //하나 생성

            if(item == PlayerJob.none)
            {
                data.text = $"수업을 선택하세요.";
            }
            else if(item == PlayerJob.potion)
            {
                data.text = $"{ChatSetting.Name<PlayerJob>(item)} 제작 수업";
            }
            else
            {
                data.text = $"{ChatSetting.Name<PlayerJob>(item)} 마법 수업";
            }
            dropdown.options.Add(data);
        }
    }
}
