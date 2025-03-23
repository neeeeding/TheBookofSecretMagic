using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreEnter : MonoBehaviour
{
    private bool isStore;

    private void Awake()
    {
        isStore = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isStore = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isStore = false;
        }
    }

    public void ClickStore()
    {
        if(isStore)
        {
            UISettingManager.Instance.Store();
        }
    }
}
