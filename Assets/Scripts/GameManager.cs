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
    public Weapon weapon;
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

    //upgrade weapon
    public bool TryUpgradeWeapon()
    {
        //is weapon max lvl?
        if (weaponPrices.Count <= weapon.weaponLevel)
            return false;
        if (pesos >= weaponPrices[weapon.weaponLevel])
        {
            pesos -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }
        //if not max lvl and enough pesos
        return false;
    }

    //experience system
    public int GetCurrentLevel()
    {
        int r = 0;
        int add = 0;
        while (experience >= add)
        {
            add += xpTable[r];
            r++;
        }
        return r;
    }
    public int GetXpToLevel(int level)
    {
        int r = 0;
        int xp = 0;
        while (r < level)
        {
            xp += xpTable[r];
            r++;
        }
        return xp;
    }
    public void GrantXp(int xp)
    {
        int currLevel = GetCurrentLevel();
        experience += xp;
        if (currLevel < GetCurrentLevel())
            OnLevelUp();
    }
    //when lvl up
    public void OnLevelUp()
    {
        player.OnLevelUp();
        // OnHitpointChange();
    }

    public void SaveState()
    {
        Debug.Log("Save state");
        string s = "";

        s += "0" + "|";
        s += pesos.ToString() + "|";
        s += experience.ToString() + "|";
        s += weapon.weaponLevel.ToString();

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
        weapon.weaponLevel = int.Parse(data[3]);
        //set level of player
        if (GetCurrentLevel() != 1)
            player.SetLevel(GetCurrentLevel());
    }
}
