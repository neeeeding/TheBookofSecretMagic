using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void SceneBtn(int i) //해당 씬으로
    {
        SceneManager.LoadScene(i);
    }
    public void SceneBtn(string name) //해당 씬으로
    {
        SceneManager.LoadScene(name);
    }

    public void QuitBtn() //끝내기
    {
        Application.Quit();
    }
}
