using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LikeabilityCard : MonoBehaviour
{
    [SerializeField] private CharacterName characterName; //대상 이름

    [SerializeField] private TextMeshProUGUI valueText; //호감도 표시 텍스트
    [SerializeField] private Slider valueSlider; //호감도 슬라이더
    [SerializeField]private TMP_InputField memo; //메모
    private int loveValue; //호감도 (수)

    public void Click()
    {
        UIManager.Instance.LiKeItme();
    }
}

public enum CharacterName
{
    resty,
    chris,
    theo,
    noah,
    nia,
    villain,
    harry,
    danie,
    pio
}
