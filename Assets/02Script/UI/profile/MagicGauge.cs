using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicGauge : MonoBehaviour
{
    [SerializeField] private Slider potionSlider;
    [SerializeField] private Slider copySlider;
    [SerializeField] private Slider waterSlider;
    [SerializeField] private Slider fireSlider;
    [SerializeField] private Slider healSlider;
    [SerializeField] private Slider blackSlider;
    private PlayerStatSC stat = null;

    private void OnEnable()
    {
        LoadCard.OnLoad += LoadStat;
        if(stat !=  null)
        {
            LoadSlider();
        }
        else if(GameManager.Instance.isStart)
        {
            LoadStat();
        }
    }

    private void LoadSlider()
    {
        potionSlider.value = stat.potionMagic;
        copySlider.value = stat.copyMagic;
        waterSlider.value = stat.waterMagic;
        fireSlider.value = stat.fireMagic;
        healSlider.value = stat.healMagic;
        blackSlider.value = stat.blackMagic;
    }

    private void LoadStat()
    {
        stat = GameManager.Instance.PlayerStat;
        LoadSlider();
    }

    private void OnDisable()
    {
        LoadCard.OnLoad -= LoadStat;
    }
}
