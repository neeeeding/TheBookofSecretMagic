using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    private bool isSceneChange; //true : 씬 이동 가능 / false : 씬 이동 불가
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
    public void SceneBtn(int i) //해당 씬으로
    {
        if(isSceneChange)
        {
            SceneManager.LoadScene(i);
        }
    }
    public void SceneBtn(string name) //해당 씬으로
    {
        if (isSceneChange)
        {
            SceneManager.LoadScene(name);
        }
    }

    public void QuitBtn() //끝내기
    {
        Application.Quit();
    }
}
