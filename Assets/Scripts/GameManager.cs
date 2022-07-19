using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;
    public Player player;
    public FloatingTextManager floatingTextManager;
    //logic
    public int pesos;
    public int experience;

    private void Awake()
    {
        //check if have gameObject
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        SceneManager.sceneLoaded += LoadState;
        //prevent from destroyed
        DontDestroyOnLoad(gameObject);
    }

    //to make as reference call function in floatingtextmanager script
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    public void SaveState()
    {
        Debug.Log("Save state");
        string s = "";

        s += "0" + "|";
        s += pesos.ToString() + "|";
        s += experience.ToString() + "|";

        PlayerPrefs.SetString("SaveState", s);
    }

    //load game
    public void LoadState(Scene s, LoadSceneMode mode)
    {
        Debug.Log("Load state");
        //check if has save data
        if (!PlayerPrefs.HasKey("SaveState"))
            return;
        string[] data = PlayerPrefs.GetString("SaveState").Split('|');
        //change player skin
        pesos = int.Parse(data[1]);
        experience = int.Parse(data[2]);
    }
}
