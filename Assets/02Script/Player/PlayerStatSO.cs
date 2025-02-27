using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatSO", menuName = "SO/PlayerStatSO")]

public class PlayerStatSO : ScriptableObject
{
    [Range(0,100)]
    public float backMagic;
    [Range(0, 100)]
    public float healMagic;
    [Range(0, 100)]
    public float fireMagic;
    [Range(0, 100)]
    public float waterMagic;
    [Range(0, 100)]
    public float copyMagic;
    [Range(0, 100)]
    public float PotionMagic;

    public int playerMoney; //소지금

    public PlayerJob job; //전공

    [ContextMenu("ResetStat")]
    public void ResetStat()
    {
        backMagic = 0;
        healMagic = 0;
        fireMagic = 0;
        waterMagic = 0;
        copyMagic = 0;
        PotionMagic = 0;

        playerMoney = 5;

        job = PlayerJob.none;
    }
}

public enum PlayerJob
{
    back, heal, fire, water, copy, potion, none
}
