using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Character : MonoBehaviour
{
    [SerializeField] private CharacterSO characterSO;
    private int love;
    private bool isChat;

    private void MyLove()
    {
    }


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
        if (isChat)
        {
            UISettingManager.Instance.Store();
        }
    }
}
