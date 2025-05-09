using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void SceneBtn(int i) //�ش� ������
    {
        SceneManager.LoadScene(i);
    }
    public void SceneBtn(string name) //�ش� ������
    {
        SceneManager.LoadScene(name);
    }

    public void QuitBtn() //������
    {
        Application.Quit();
    }
}
