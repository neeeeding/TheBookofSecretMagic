using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DormitoryManager : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private int roleHour = 10; //�Ͱ� �ؾ� �ϴ� �ð�

    private void Awake()
    {
        CheckClock(); // �ʰ� �Դ��� (���߿� �ܹ� �� ���� ������ �װ͵� Ȯ��
    }


    private void CheckClock() //�ð� Ȯ��
    {
       int hour = GameManager.Instance.PlayerStat.hour;
       int minute = GameManager.Instance.PlayerStat.minute;


        if(hour > roleHour)
        {
            print($"���� {hour}�ð� �Ѿ��µ� ������ ���°ž�?!");
        }
        else
        {
            print("����, �� �ð��� ���� �Գ�.");
        }
    }
}
