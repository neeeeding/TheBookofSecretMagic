using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatBtn : MonoBehaviour
{
    public static Action OnSkipChat;

    [Header("SaveBtn Need")]
    [SerializeField] private GameObject SaveWindow;
    [Space(0f)]
    [Header("Hide Need")]
    [SerializeField] private GameObject chatObj;
    [SerializeField] private GameObject[] inGame;

    private void Awake()
    {
        CloseSave();
    }

    public void LoadBtn() //�ε� ��ư ���� ��
    {
        UISettingManager.Instance.Save();
    }

    public void SaveBtn() //���� ��ư
    {
        SaveWindow.SetActive(true);
    }

    public void CloseSave() //���� â �ݱ�
    {
        SaveWindow.SetActive(false);
    }

    public void SkipBtn() //��ŵ
    {
        OnSkipChat?.Invoke();
    }

    public void HideBtn() //��Ȱ��ȭ ��ư
    {
        Show(false);
    }

    public void ShowBtn() //Ȱ��ȭ
    {
        Show(true);
    }

    private void Show(bool show)
    {
        chatObj.SetActive(show);
        foreach (GameObject obj in inGame)
        {
            obj.SetActive(show);
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
