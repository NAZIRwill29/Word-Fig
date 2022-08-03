using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//for exit
#if UNITY_EDITOR
using UnityEditor;
#endif
public class MainMenuController : MonoBehaviour
{
    private string sceneName = "MainScene";
    //start game
    public void StartGame()
    {
        //Debug.Log(sceneName);
        SceneManager.LoadScene(sceneName);
        GameManager.instance.OnStartGame();
    }
    //exit game
    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
    //reset game
    public void ResetGame()
    {
        GameManager.instance.ResetSaveState();
    }

    //get sceneName from backToMenuButton
    // public void PassSceneName(string name)
    // {
    //     sceneName = name;
    //     Debug.Log(sceneName);
    // }
}
