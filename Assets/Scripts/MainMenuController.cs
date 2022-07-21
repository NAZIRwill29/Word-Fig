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
    //start game
    public void StartGame()
    {
        SceneManager.LoadScene(1);
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
}
