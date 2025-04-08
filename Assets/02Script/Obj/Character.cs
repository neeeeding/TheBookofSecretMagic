using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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

    private void Awake()
    {
        isChat = false;
        path = $"{characterSO.characterName}Dialog";
        finallNum = PlayerPrefs.GetInt($"{path}finallNum");
        chapter = PlayerPrefs.GetInt($"{path}chapter");
    }

    public void Load() //�ε� �� ��
    {
        FieldInfo field = GameManager.Instance.FindCharacterLastText(characterSO.characterName);

        if (field != null)
        {
            int[] chapterNum = (int[])field.GetValue(GameManager.Instance.PlayerStat);
            chapter = chapterNum[0];
            finallNum = chapterNum[1];
            PlayerPrefs.SetInt($"{path}chapter", chapter);
            PlayerPrefs.SetInt($"{path}finallNum", finallNum);
            PlayerPrefs.Save();
        }
    }

    public void NextDialog(int i) //��ȭ�� ���� �� ������
    {
        finallNum = i;
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

    public void NextChapter() //���� é�ͷ� ���� ���ֱ�
    {
        finallNum = 1;
        chapter++;
        PlayerPrefs.SetInt($"{path}chapter", chapter);
        PlayerPrefs.SetInt($"{path}finallNum", finallNum);
        PlayerPrefs.Save();
    }

    public void ClickCharacter() //��ȭ �ϱ� (Ŭ��)
    {
        if (isChat)
        {
            //finallNum = 1;
            OnChat?.Invoke(characterSO,this);
        }
    }

    private void OnEnable()
    {
        LoadCard.OnLoad += Load;
    }

    private void OnDisable()
    {
        LoadCard.OnLoad -= Load;
    }
}
