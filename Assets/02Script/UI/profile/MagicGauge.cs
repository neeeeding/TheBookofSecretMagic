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
        if(GameManager.Instance.isStart)
        {
            LoadSlider();
        }
    }

    private void LoadSlider()
    {
        //·Îµå
        stat = GameManager.Instance.PlayerStat;
        potionSlider.value = stat.potionMagic;
        copySlider.value = stat.copyMagic;
        waterSlider.value = stat.waterMagic;
        fireSlider.value = stat.fireMagic;
        healSlider.value = stat.healMagic;
        blackSlider.value = stat.blackMagic;
    }

    public void PotionValue()
    {
        stat = GameManager.Instance.PlayerStat;
        stat.potionMagic = potionSlider.value;
    }
    public void CopyValue()
    {
        stat = GameManager.Instance.PlayerStat;
        stat.copyMagic = copySlider.value;
    }
    public void WaterValue()
    {
        stat = GameManager.Instance.PlayerStat;
        stat.waterMagic = waterSlider.value;
    }
    public void FireValue()
    {
        stat = GameManager.Instance.PlayerStat;
        stat.fireMagic = fireSlider.value;
    }
    public void HealValue()
    {
        stat = GameManager.Instance.PlayerStat;
        stat.healMagic = healSlider.value;
    }
    public void BlackValue()
    {
        stat = GameManager.Instance.PlayerStat;
        stat.blackMagic = blackSlider.value;
    }
}
