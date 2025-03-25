using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Character : MonoBehaviour
{
    public static Action<CharacterSO> OnChat;
    [SerializeField] private CharacterSO characterSO;
    private bool isChat;


    private void Awake()
    {
        isChat = false;
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

    public void ClickCharacter()
    {
        print("d");
        if (isChat)
        {
            OnChat?.Invoke(characterSO);
        }
    }
}
