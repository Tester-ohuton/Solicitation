using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleDirector : MonoBehaviour
{
    public void StartButton(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /*ƒ{ƒ^ƒ“‚©‚çŒÄ‚Ô*/
    public void OnQuit()
    {
        Application.Quit();
    }
}
