using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    private bool isSceneChange; //true : �� �̵� ���� / false : �� �̵� �Ұ�

    private void Awake()
    {
        //LoadScene();
        LoadCard.OnLoad += LoadScene;
    }

    private void OnDisable()
    {
        LoadCard.OnLoad -= LoadScene;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isSceneChange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isSceneChange = false;
        }
    }

    private void LoadScene() //�� �ε�
    {
        string scene = GameManager.Instance.PlayerStat.sceneName;
        print($"cu : {scene} / sa {GameManager.Instance.PlayerStat.sceneName}");

        if (scene != GameManager.Instance.curScene) //���� ���� �̵��ؾ��ϴ� ���� �� �´ٸ�
        {
            isSceneChange = true;
            SceneBtn(scene);
        }
    }

    public void SceneBtn(int i) //�ش� ������
    {
        if(isSceneChange)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneName = Path.GetFileNameWithoutExtension(scenePath);
            GameManager.Instance.curScene = sceneName;
            GameManager.Instance.PlayerStat.sceneName = sceneName;
            SceneManager.LoadScene(i);
        }
    }
    public void SceneBtn(string name) //�ش� ������
    {
        if (isSceneChange)
        {
            GameManager.Instance.curScene = name;
            GameManager.Instance.PlayerStat.sceneName = name;
            SceneManager.LoadScene(name);
        }
    }

    public void QuitBtn() //������
    {
        Application.Quit();
    }
}
