using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatBtn : MonoBehaviour
{
    [SerializeField] private GameObject chatObj;
    [SerializeField] private GameObject[] inGame;

    public void LoadBtn() //로드 버튼 누를 때
    {
        UISettingManager.Instance.Save();
    }

    public void SaveBtn() //저장
    {

    }

    public void SkipBtn() //스킵
    {

    }

    public void HideBtn() //비활성화 버튼
    {
        chatObj.SetActive(false);
        foreach(GameObject obj in inGame)
        {
            obj.SetActive(false);
        }
    }

    public void ShowBtn() //활성화
    {
        chatObj.SetActive(true);
        foreach (GameObject obj in inGame)
        {
            obj.SetActive(true);
        }
    }

    public void Likeability() //호감도 도감
    {
        UISettingManager.Instance.LikeabilityGuide();
    }

    public void LoveBtn() //선호 보기
    {

    }
}
