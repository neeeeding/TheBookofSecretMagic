using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Character : MonoBehaviour
{
    //현재 memo, love, dialogfinallName, dialogchapter 있음
    public static Action<CharacterSO,Character> OnChat;

    [SerializeField] private CharacterSO characterSO;
    [SerializeField] private int chapter; //챕터
    [SerializeField] private int finallNum; //번호
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

    public void Load() //로드 될 때
    {

    }

    public void NextDialog() //대화가 진행 될 때마다
    {
        finallNum++;
        PlayerPrefs.SetInt($"{path}finallNum", finallNum);
        PlayerPrefs.Save();
    }

    public int[] CurrentDialog() //현재 진행 사항 (챕터, 넘버 값 넘겨주기)
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

    public void ClickCharacter() //대화 하기 (클릭)
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
