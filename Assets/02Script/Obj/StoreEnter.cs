using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreEnter : MonoBehaviour
{
    [SerializeField] private ButtonClickUI clickUI;

    public void ClickStore()
    {
        clickUI.Store();
    }
}
