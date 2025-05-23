using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LikeabilityCard : MonoBehaviour
{
    [SerializeField] private CharacterSO character; //��� ����

    [SerializeField] private TextMeshProUGUI characterName; //��� �̸�
    [SerializeField] private Image characterImage;
    [SerializeField] private TextMeshProUGUI valueText; //ȣ���� ǥ�� �ؽ�Ʈ
    [SerializeField] private Slider valueSlider; //ȣ���� �����̴�
    [SerializeField]private TMP_InputField memo; //�޸�
    private int loveValue; //ȣ���� (��)

    private PlayerStatSC path; //���� (���� ����)

    private void Awake()
    {
        characterName.text = ChatSetting.Name(character.characterName);
        //characterImage.sprite = character.characterImage;
        GameManager.OnStart += LoadData;
    }

    private void OnEnable()
    {
        LoadCard.OnLoad += LoadData;
        if(GameManager.Instance.isStart)
        {
            path = GameManager.Instance.PlayerStat;
            memo.text = path.characterlastText[character.characterName][DialogType.Memo];
            LoadData(); // �ε带 ����
        }
    }

    public void Click()
    {
        UISettingManager.Instance.LiKeItme(character);
    }

    public void InputText()
    {
        string value = memo.text;
        path.characterlastText[character.characterName][DialogType.Memo] = value; //�޸� ����
    }

    private void LoveUp(int value)
    {
        loveValue += value;
        valueText.text = $"{loveValue} / 100 ";
        valueSlider.value = loveValue;

        path.characterlastText[character.characterName][DialogType.Love] = loveValue.ToString(); //ȣ���� ����

        SaveMyLoveValue(true);
    }

    private void LoadData()
    {
        SaveMyLoveValue(false);
    }

    private void SaveMyLoveValue(bool set)
    {
        if(path != null)
        {
            if(set)
            {
                path.characterlastText[character.characterName][DialogType.Love] = loveValue.ToString(); //ȣ���� ����
            }
            else
            {
                int.TryParse(path.characterlastText[character.characterName][DialogType.Love], out loveValue); //ȣ���� ����
                LoveUp(0);
            }
        }
    }

    private void OnDisable()
    {
        GameManager.OnStart -= LoadData;
        LoadCard.OnLoad += LoadData;
    }
}
