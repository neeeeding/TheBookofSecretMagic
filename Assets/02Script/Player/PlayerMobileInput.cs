using System;
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
        canInput = false;
    }

    public void CanInput()
    {
        canInput = true;
    }

    public bool CheckCanInput()
    {
        return canInput;
    }
}
