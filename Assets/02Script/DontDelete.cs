using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDelete : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
