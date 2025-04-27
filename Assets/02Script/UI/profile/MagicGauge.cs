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
        stat = GameManager.Instance.PlayerStat;
        potionSlider.value = stat.potionMagic;
        copySlider.value = stat.copyMagic;
        waterSlider.value = stat.waterMagic;
        fireSlider.value = stat.fireMagic;
        healSlider.value = stat.healMagic;
        blackSlider.value = stat.blackMagic;

        PlayerPrefs.SetFloat(nameof(stat.potionMagic),stat.potionMagic);
        PlayerPrefs.SetFloat(nameof(stat.copyMagic),stat.copyMagic);
        PlayerPrefs.SetFloat(nameof(stat.waterMagic),stat.waterMagic);
        PlayerPrefs.SetFloat(nameof(stat.fireMagic),stat.fireMagic);
        PlayerPrefs.SetFloat(nameof(stat.healMagic),stat.healMagic);
        PlayerPrefs.SetFloat(nameof(stat.blackMagic),stat.blackMagic);
    }
}
