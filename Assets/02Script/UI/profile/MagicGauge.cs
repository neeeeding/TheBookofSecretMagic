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
    private PlayerStatSC saveStat = null;

    private void OnEnable()
    {
        if(GameManager.Instance.isStart)
        {
            LoadSlider();
        }
    }

    private void LoadSlider()
    {
        //로드
        stat = GameManager.Instance.PlayerStat;
        potionSlider.value = stat.potionMagic;
        copySlider.value = stat.copyMagic;
        waterSlider.value = stat.waterMagic;
        fireSlider.value = stat.fireMagic;
        healSlider.value = stat.healMagic;
        blackSlider.value = stat.blackMagic;

        //저장
        saveStat = GameManager.Instance.saveData.stat;
        saveStat.potionMagic= potionSlider.value;
        saveStat.copyMagic  = copySlider.value;
        saveStat.waterMagic = waterSlider.value;
        saveStat.fireMagic  = fireSlider.value;
        saveStat.healMagic  = healSlider.value;
        saveStat.blackMagic = blackSlider.value;
    }
}
