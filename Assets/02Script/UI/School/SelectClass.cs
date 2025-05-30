using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class SelectClass : MonoBehaviour
{
    [Header("Need")]
    [SerializeField] private TMP_Dropdown[] select; // 해당 드롭 다운
    [SerializeField] private GameObject warning; // 안 씀 경고

    private void Awake()
    {
        warning.SetActive(false);
        foreach (TMP_Dropdown item in select)
        {
            SetClass(item);
        }

    }

    //없음 일 때가 안되도록 막기
    public void CompleteBtn() //완료 버튼 눌렀을 때
    {
        SchoolManager.Instance.ClearToday(); //혹시 모르니 비워주기
        bool isNone = false; // true : none가 포함 / false : none가 미포함

        for(int i = 1; i <=select.Length; i++)
        {
            TMP_Dropdown item = select[i-1];

            string value = item.options[item.value].text;
            PlayerJob v = value switch //포함하는 글자를 보고 변환
            {
                string s when s.Contains("흑") => PlayerJob.brack,
                string s when s.Contains("치료") => PlayerJob.heal,
                string s when s.Contains("불") => PlayerJob.fire,
                string s when s.Contains("물") => PlayerJob.water,
                string s when s.Contains("복제") => PlayerJob.copy,
                string s when s.Contains("포션") => PlayerJob.potion,
                _ => PlayerJob.none
            };

            if (v == PlayerJob.none)
            {
                isNone = true;
                break;
            }

            SchoolManager.Instance.todayClass.Add(i,v ); //추가하기
        }

        if (!isNone) // 전부 제대로 선택 할 때
        {

            SchoolManager.Instance.SettingTodayClass(); //수업 세팅한 것을 알려주기
            UISettingManager.Instance.InGame();
        }
        else
        {
            StartCoroutine(Warning());
        }
    }

    private IEnumerator Warning()
    {
        warning.SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        warning.SetActive(false);
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
