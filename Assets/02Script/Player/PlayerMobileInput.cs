using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMobileInput : Singleton<PlayerMobileInput>
{
    public static Action<Vector2> mousePos;

    private bool canInput;

    private void Awake()
    {
        CanInput();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canInput)
        {
            print(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            mousePos?.Invoke(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    public void NoInput()
    {
        print("¸ØÃç");
        canInput = false;
    }

    public void CanInput()
    {
        print("¹Ù·Î°¡º¸ÀÚ");
        canInput = true;
    }
}
