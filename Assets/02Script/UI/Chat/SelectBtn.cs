using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectBtn : MonoBehaviour
{
    public static Action<int> OnSelect; //������ ��ȣ

    private int mySelectNum; //������ ��ȣ (é�Ϳ���, �ѹ�)
    [SerializeField] private TextMeshProUGUI selectTexts; //������ ��ȭ

    public void SetSelect(string text, int thisNum) //��ȭ ����, ������ ��ȣ
    {
        selectTexts.text = text;
        mySelectNum = thisNum;
    }

    public void ClcikSelect() //���� ��ư ���� ��
    {
        OnSelect?.Invoke(mySelectNum + 1/*0���� �����ϴ�*/);
    }
}
