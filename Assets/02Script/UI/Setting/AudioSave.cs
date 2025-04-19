using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSave : MonoBehaviour
{
    [SerializeField] private Slider main;
    [SerializeField] private Slider bgm;
    [SerializeField] private Slider effect;
    private string path;

    private void Awake()
    {
        path = "Sound";
        main.value = PlayerPrefs.GetFloat($"{main}{path}");
        bgm.value = PlayerPrefs.GetFloat($"{bgm}{path}");
        effect.value = PlayerPrefs.GetFloat($"{effect}{path}");
    }

    public void ChangeValue()
    {
        if(GameManager.Instance.isStart)
        {
            PlayerPrefs.SetFloat($"{main}{path}", main.value);
            PlayerPrefs.SetFloat($"{bgm}{path}", bgm.value);
            PlayerPrefs.SetFloat($"{effect}{path}", effect.value);
            PlayerPrefs.Save();
        }
    }
}
