using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System;

public class Save : MonoBehaviour
{
    public static string path;
    [SerializeField] private GameObject nameMessage; //�����̸� �ۼ� �޽���
    [SerializeField] private GameObject deleteWindow; //���� Ȯ�� â
    [SerializeField] private GameObject oneLoadCard; //���̺� ���� �ϳ�
    [SerializeField] private GameObject content; //����

    [SerializeField] private TMP_InputField inputName; //���ϸ�

    private string[] fileNames; //���� ���� �̸���
    private LoadCard deleteCard; //������ ���� ���� (�ӽ�)

    private void Awake()
    {
        path = Application.persistentDataPath + "/Save";
        print(path);

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
        inputName.text = "";
        nameMessage.SetActive(true);
    }

    public void CompleteBtn() //(�̸� �ۼ�) �Ϸ� ��ư
    {
        if(inputName.text != "")
        {
            GameObject loadCard = Instantiate(oneLoadCard, content.transform);
            loadCard.GetComponent<LoadCard>().ClcikSave(inputName.text);
            File.AppendAllText($"{path}/SaveName", $"{inputName.text}\n");

            CloseMessage();
        } //���ϸ� ��ġ�°� ���
    }

    public void CloseMessage() //��� â (�̸��ۼ�, ���� Ȯ��) �ݱ�
    {
        nameMessage.SetActive(false);
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

    private void OnDisable()
    {
        LoadCard.OnDelete -= DeleteLoad;
    }
}
