using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class Save : MonoBehaviour
{
    public static string path;
    [SerializeField] private GameObject nameMessage; //파일이름 작성 메시지
    [SerializeField] private GameObject oneLoadCard; //세이브 파일 하나
    [SerializeField] private GameObject content; //보관

    [SerializeField] private TMP_InputField inputName; //파일명

    private string[] fileNames; //저장 파일 이름들

    private void Awake()
    {
        path = Application.persistentDataPath + "/Save";

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path); // 폴더 생성
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
        } //파일명 겹치는거 고려
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
