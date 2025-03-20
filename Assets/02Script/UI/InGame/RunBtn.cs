using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunBtn : MonoBehaviour
{
    public void Run()
    {
        GameManager.Instance.PlayerStat.playerSpeed = 5f; 
    }

    public void Walk()
    {
        GameManager.Instance.PlayerStat.playerSpeed = 1f;
    }
}
