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
