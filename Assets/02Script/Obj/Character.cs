using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Character : MonoBehaviour
{
    //���� memo, love, dialogfinallName, dialogchapter ����
    public static Action<CharacterSO,Character> OnChat;

    [SerializeField] private CharacterSO characterSO;
    [SerializeField] private int chapter; //é��
    [SerializeField] private int finallNum; //��ȣ
    private string path;
    private bool isChat;

    [ContextMenu("ResetAll")]
    private void R()
    {
        finallNum = 1;
        PlayerPrefs.SetInt($"{path}finallNum", finallNum);
        chapter = 0;
        PlayerPrefs.SetInt($"{path}chapter", chapter);
        PlayerPrefs.Save();
    }


    private void Awake()
    {
        isChat = false;
        path = $"{characterSO.characterName}Dialog";
        finallNum = PlayerPrefs.GetInt($"{path}finallNum");
        chapter = PlayerPrefs.GetInt($"{path}chapter");

    }

    public void Load() //�ε� �� ��
    {

    }

    public void NextDialog() //��ȭ�� ���� �� ������
    {
        finallNum++;
        PlayerPrefs.SetInt($"{path}finallNum", finallNum);
        PlayerPrefs.Save();
    }

    public int[] CurrentDialog() //���� ���� ���� (é��, �ѹ� �� �Ѱ��ֱ�)
    {
        return new int[]{chapter, finallNum};
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isChat = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isChat = false;
        }
    }

    public void ClickCharacter() //��ȭ �ϱ� (Ŭ��)
    {
        if (isChat)
        {
            OnChat?.Invoke(characterSO,this);
            chapter++;
            finallNum = 1;
            PlayerPrefs.SetInt($"{path}chapter", chapter);
            PlayerPrefs.Save();
        }
    }
}
