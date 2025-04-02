using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatBtn : MonoBehaviour
{
    public static Action OnSkipChat;

    [SerializeField] private GameObject chatObj;
    [SerializeField] private GameObject[] inGame;

    public void LoadBtn() //�ε� ��ư ���� ��
    {
        UISettingManager.Instance.Save();
    }

    public void SaveBtn() //����
    {

    }

    public void SkipBtn() //��ŵ
    {
        OnSkipChat?.Invoke();
    }

    public void HideBtn() //��Ȱ��ȭ ��ư
    {
        chatObj.SetActive(false);
        foreach(GameObject obj in inGame)
        {
            obj.SetActive(false);
        }
    }

    public void ShowBtn() //Ȱ��ȭ
    {
        chatObj.SetActive(true);
        foreach (GameObject obj in inGame)
        {
            obj.SetActive(true);
        }
    }

    public void Likeability() //ȣ���� ����
    {
        UISettingManager.Instance.LikeabilityGuide();
    }

    public void LoveBtn() //��ȣ ����
    {

    }
}
