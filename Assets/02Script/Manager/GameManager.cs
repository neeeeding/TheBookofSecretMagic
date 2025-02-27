using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> 
{
    public PlayerStatSO PlayerStat;

    public Player Player;

    private void Awake()
    {
        Player = gameObject.GetComponent<Player>();
    }
}

