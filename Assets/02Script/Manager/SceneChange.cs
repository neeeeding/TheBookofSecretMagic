using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    private bool isSceneChange; //true : �� �̵� ���� / false : �� �̵� �Ұ�
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
    public void SceneBtn(int i) //�ش� ������
    {
        if(isSceneChange)
        {
            SceneManager.LoadScene(i);
        }
    }
    public void SceneBtn(string name) //�ش� ������
    {
        if (isSceneChange)
        {
            SceneManager.LoadScene(name);
        }
    }

    public void QuitBtn() //������
    {
        Application.Quit();
    }
}
