using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LikeabilityCard : MonoBehaviour
{
    [SerializeField] private CharacterName characterName; //��� �̸�

    [SerializeField] private TextMeshProUGUI valueText; //ȣ���� ǥ�� �ؽ�Ʈ
    [SerializeField] private Slider valueSlider; //ȣ���� �����̴�
    [SerializeField]private TMP_InputField memo; //�޸�
    private int loveValue; //ȣ���� (��)

    private void Awake()
    {
        LoveUp(0);
    }

    public void Click()
    {
        UIManager.Instance.LiKeItme();
    }

    public void InputText()
    {
        string value = memo.text;
        PlayerPrefs.SetString(characterName.ToString(), value);
    }

    private void LoveUp(int value)
    {
        loveValue = value;
        valueText.text = $"{loveValue} / 100 ";
        valueSlider.value = loveValue / 100;
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
