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
    public string sceneName;
    //start game
    public void StartGame(string name)
    {
        PassSceneName(name);
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

    //get sceneName from other
    public void PassSceneName(string name)
    {
        sceneName = name;
        //Debug.Log(sceneName);
    }
}
