using UnityEngine;
using TMPro;
using System.IO;

public class Save : MonoBehaviour
{
    [Header("All Need")]
    [SerializeField] private GameObject deleteWindow; //���� Ȯ�� â
    [SerializeField] private GameObject oneLoadCard; //���̺� ���� �ϳ�
    [SerializeField] private GameObject content; //����
    [Space(20f)]
    [Header("Save_save Need")]
    [SerializeField] private TMP_InputField saveInputName; //�⺻ ���� ���ϸ�
    [SerializeField] private GameObject saveNameMessage; //�����̸� �ۼ� �޽���
    [Space(20f)]
    [Header("Chat_save Need")]
    [SerializeField] private TMP_InputField chatInputName; //ä�� ���� ���ϸ�
    [SerializeField] private GameObject chatNameMessage; //�����̸� �ۼ� �޽���

    private string path; //���� ���� ��ġ
    private string[] fileNames; //���� ���� �̸���
    private LoadCard deleteCard; //������ ���� ���� (�ӽ�)

    private void Awake()
    {
        SettingPath();

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path); // ���� ����
        }

        if (File.Exists($"{path}/saveName"))
        {
            fileNames = File.ReadAllLines($"{path}/saveName");
            LoadSave();
        }

        CloseMessage();
    }
    private void OnEnable()
    {
        LoadCard.OnDelete += DeleteLoad;
    }

    public void DeleteBtn() //���� (��)
    {
        deleteCard.DeleteMe();
        CloseMessage();
    }

    public void SaveBtn() //���� ��ư ���� �� (�̸� �ۼ� â �߱�)
    {
        saveNameMessage.SetActive(true);
    }

    public void CompleteBtn() //(�̸� �ۼ�) �Ϸ� ��ư
    {
        TMP_InputField inputName = chatInputName;
        if (saveInputName.text != "")
        {
            inputName = saveInputName;
        }
        if (inputName.text != "")
        {
            GameObject loadCard = Instantiate(oneLoadCard, content.transform);
            loadCard.GetComponent<LoadCard>().ClcikSave(inputName.text);
            SettingPath(); //Ȥ�� �𸣴� �� ����
            File.AppendAllText($"{path}/SaveName", $"{inputName.text}\n");

            CloseMessage();
            inputName.text = "";
        } //���ϸ� ��ġ�°� ���
    }

    public void CloseMessage() //��� â (�̸��ۼ�, ���� Ȯ��) �ݱ�
    {
        saveNameMessage.SetActive(false);
        chatNameMessage.SetActive(false);
        deleteCard = null;
        deleteWindow.SetActive(false);
    }

    private void LoadSave() //�����ϱ�
    {
        for(int i = 0; i < fileNames.Length; i++)
        {
            GameObject loadCard = Instantiate(oneLoadCard, content.transform);

            string data = File.ReadAllText($"{path}/{fileNames[i]}");
            PlayerStatSC stat = JsonUtility.FromJson<PlayerStatSC>(data);

            loadCard.GetComponent<LoadCard>().AwakeLoadSave(fileNames[i],stat );
        }
    }

    private void DeleteLoad(LoadCard card) //ī�� �����ϱ� â
    {
        deleteWindow.SetActive(true);
        deleteCard = card;
        
    }

    private void SettingPath()
    {
        if (GameManager.GameSaveFilePath != null && GameManager.GameSaveFilePath != "" && path != "")
        {
            path = GameManager.GameSaveFilePath;
        }
        else
        {
            path = Application.persistentDataPath + "/Save";
        }
    }

    private void OnDisable()
    {
        LoadCard.OnDelete -= DeleteLoad;
    }
}
