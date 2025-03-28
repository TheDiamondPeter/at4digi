using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenSwitching : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    } 
}

