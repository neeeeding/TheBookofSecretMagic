using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class Save : MonoBehaviour
{
    public static string path;
    [SerializeField] private GameObject nameMessage; //�����̸� �ۼ� �޽���
    [SerializeField] private GameObject oneLoadCard; //���̺� ���� �ϳ�
    [SerializeField] private GameObject content; //����

    [SerializeField] private TMP_InputField inputName; //���ϸ�

    private string[] fileNames; //���� ���� �̸���

    private void Awake()
    {
        path = Application.persistentDataPath + "/Save";

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

    public void SaveBtn()
    {
        inputName.text = "";
        nameMessage.SetActive(true);
    }

    public void CompleteBtn()
    {
        if(inputName.text != "")
        {
            GameObject loadCard = Instantiate(oneLoadCard, content.transform);
            loadCard.GetComponent<LoadCard>().ClcikSave(inputName.text);
            File.AppendAllText($"{Save.path}/SaveName", $"{inputName.text}\n");

            CloseMessage();
        } //���ϸ� ��ġ�°� ���
    }

    public void CloseMessage()
    {
        nameMessage.SetActive(false);
    }

    private void LoadSave()
    {
        for(int i = 0; i < fileNames.Length; i++)
        {
            GameObject loadCard = Instantiate(oneLoadCard, content.transform);
            loadCard.GetComponent<LoadCard>().ClcikSave(fileNames[i]);
        }
    }
}
