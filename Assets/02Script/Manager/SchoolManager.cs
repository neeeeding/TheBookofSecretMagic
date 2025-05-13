using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchoolManager : Singleton<SchoolManager>
{
    public SaveDictionary<int,PlayerJob> todayClass; //���� ����
    public bool setTodayClass; // true : ���� ���� ���� ��/ false : ������ ���� ���� �� ��.

    public void ClearToday() //�б� ���� �� ����
    {
        todayClass.Clear();
    }

    public void SettingTodayClass() //���� ���� �� ���� �˷��ֱ�.
    {
        setTodayClass = true;
        //���� �� UI ��� ����.
    }
}
