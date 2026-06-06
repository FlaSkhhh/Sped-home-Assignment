using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    
    public void StartTask1()
    {
        SceneManager.LoadScene(1);
    }

    public void StartTask2()
    {
        SceneManager.LoadScene(2);
    }
}
