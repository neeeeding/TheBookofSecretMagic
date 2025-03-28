using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System;

public class Save : MonoBehaviour
{
    public static string path;
    [SerializeField] private GameObject nameMessage; //파일이름 작성 메시지
    [SerializeField] private GameObject deleteWindow; //삭제 확인 창
    [SerializeField] private GameObject oneLoadCard; //세이브 파일 하나
    [SerializeField] private GameObject content; //보관

    [SerializeField] private TMP_InputField inputName; //파일명

    private string[] fileNames; //저장 파일 이름들
    private LoadCard deleteCard; //삭제할 저장 파일 (임시)

    private void Awake()
    {
        path = Application.persistentDataPath + "/Save";
        print(path);

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
    private void OnEnable()
    {
        LoadCard.OnDelete += DeleteLoad;
    }

    public void DeleteBtn() //삭제 (예)
    {
        deleteCard.DeleteMe();
        CloseMessage();
    }

    public void SaveBtn() //저장 버튼 누를 때 (이름 작성 창 뜨기)
    {
        inputName.text = "";
        nameMessage.SetActive(true);
    }

    public void CompleteBtn() //(이름 작성) 완료 버튼
    {
        if(inputName.text != "")
        {
            GameObject loadCard = Instantiate(oneLoadCard, content.transform);
            loadCard.GetComponent<LoadCard>().ClcikSave(inputName.text);
            File.AppendAllText($"{path}/SaveName", $"{inputName.text}\n");

            CloseMessage();
        } //파일명 겹치는거 고려
    }

    public void CloseMessage() //모든 창 (이름작성, 삭제 확인) 닫기
    {
        nameMessage.SetActive(false);
        deleteCard = null;
        deleteWindow.SetActive(false);
    }

    private void LoadSave() //저장하기
    {
        for(int i = 0; i < fileNames.Length; i++)
        {
            GameObject loadCard = Instantiate(oneLoadCard, content.transform);

            string data = File.ReadAllText($"{path}/{fileNames[i]}");
            PlayerStatSC stat = JsonUtility.FromJson<PlayerStatSC>(data);

            loadCard.GetComponent<LoadCard>().AwakeLoadSave(fileNames[i],stat );
        }
    }

    private void DeleteLoad(LoadCard card) //카드 삭제하기 창
    {
        deleteWindow.SetActive(true);
        deleteCard = card;
        
    }

    private void OnDisable()
    {
        LoadCard.OnDelete -= DeleteLoad;
    }
}
