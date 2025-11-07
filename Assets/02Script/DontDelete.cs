using UnityEngine;


namespace _02Script.UIGame
{
    public class DontDelete : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
