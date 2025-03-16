using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreEnter : MonoBehaviour
{
    [SerializeField] private UISettingManager clickUI;

    public void ClickStore()
    {
        clickUI.Store();
    }
}
