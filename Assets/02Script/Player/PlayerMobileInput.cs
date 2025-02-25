using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMobileInput : MonoBehaviour
{
    public static Action<Vector2> mousePos;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePos?.Invoke(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }
}
