using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Character : MonoBehaviour
{
    //������ num : (é����) �ѹ�/ chapter : é��/ text : ������ ��ȭ
    //���� memo, love
    public static Action<CharacterSO,Character> OnChat;

    [SerializeField] private CharacterSO characterSO;
    [SerializeField] private int chapter; //é��
    [SerializeField] private int finallNum; //��ȣ
    private PlayerStatSC path; //���� (���� ����)
    private bool isChat;

    private void Awake()
    {
        isChat = false;
        path = GameManager.Instance.PlayerStat;
        chapter = path.characterlastText[characterSO.characterName][DialogType.Chapter];
        finallNum = path.characterlastText[characterSO.characterName][DialogType.Num];
    }

    public void Load() //�ε� �� ��
    {
        path = GameManager.Instance.PlayerStat; // 

        if (path != null)
        {
            chapter = path.characterlastText[characterSO.characterName][DialogType.Chapter];
            finallNum = path.characterlastText[characterSO.characterName][DialogType.Num];
        }

        //�������̳� Ư�� ��ȭ������ ������ ������ Ȯ�� �� ��
        if (GameManager.Instance.PlayerStat.isChat)
        {
            UISettingManager.Instance.CloseChat();
            finallNum--; //��ȭ�� ���� �� �� 1�� �߰��ϰ� ����������.
            UISettingManager.Instance.Chat(GameManager.Instance.PlayerStat.lastSO, GameManager.Instance.PlayerStat.lastCharacter);
        }
        else
        {
            UISettingManager.Instance.CloseChat();
        }
    }

    public void NextDialog(int i) //��ȭ�� ���� �� ������
    {
        finallNum = i;

        path.characterlastText[characterSO.characterName][DialogType.Num] = finallNum;
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

        path.characterlastText[characterSO.characterName][DialogType.Chapter] = chapter;
        path.characterlastText[characterSO.characterName][DialogType.Num] = finallNum;
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
