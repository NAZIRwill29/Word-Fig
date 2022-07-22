using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

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
    public RectTransform hitpointBar;
    public GameObject canvas;
    public Animator deathMenuAnim;
    public Text infoLevelText;
    public InputField userNameInput;
    public Text userNameText;
    public string userName;
    // private string dataSave;

    //required for JsonUtility
    //it will only transform things to JSON if they are tagged as Serializable.
    [System.Serializable]
    class SaveData
    {
        public string userName;
        public int pesos;
        public int experience;
        public int weaponLevel;
    }

    private void Awake()
    {
        //check if have gameObject
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(canvas);
            return;
        }
        instance = this;
        SceneManager.sceneLoaded += LoadState;
        SceneManager.sceneLoaded += OnSceneLoaded;
        //userNameInput.GetComponentInChildren<Text>().text = userName;
    }

    //when on game turn on player and canvas
    public void OnStartGame()
    {
        SceneManager.sceneLoaded += LoadState;
        player.gameObject.SetActive(true);
        canvas.SetActive(true);
        //fill in info
        infoLevelText.text = "Lv " + GetCurrentLevel().ToString();
        userName = userNameInput.text;
        userNameText.text = userName;
    }
    //when on MainMenu scene turn off player and canvas
    public void OnMainMenu()
    {
        player.gameObject.SetActive(false);
        canvas.SetActive(false);
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
        //for raise hp after level up 
        OnHitpointChange();
        //set leveltext at info
        infoLevelText.text = "Lv " + GetCurrentLevel().ToString();
    }
    //hitpoint bar
    public void OnHitpointChange()
    {
        float ratio = (float)player.hitpoint / (float)player.maxHitpoint;
        hitpointBar.localScale = new Vector3(ratio, 1, 1);
    }
    //death menu and respawn
    public void Respawn()
    {
        deathMenuAnim.SetTrigger("Hide");
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
        player.Respawn();
    }

    public void SaveState()
    {
        //-----------------------
        // dataSave = "";

        // dataSave += "0" + "|";
        // dataSave += pesos.ToString() + "|";
        // dataSave += experience.ToString() + "|";
        // dataSave += weapon.weaponLevel.ToString() + "|";
        // dataSave += userName;

        // PlayerPrefs.SetString("SaveState", dataSave);
        //-----------------------

        //save variable
        SaveData data = new SaveData();
        data.userName = userName;
        data.pesos = pesos;
        data.experience = experience;
        data.weaponLevel = weapon.weaponLevel;
        //transform instance to json
        string json = JsonUtility.ToJson(data);
        //method to write string to a file
        /*Application.persistentDataPath - give you a folder where you can save data that 
        will survive between application reinstall or update and append to it the filename savefile.json*/
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        Debug.Log("Save state");
    }

    public void ResetSaveState()
    {
        pesos = 0;
        experience = 0;
        weapon.weaponLevel = 0;
        userName = "";
        SaveState();
        Debug.Log("Resetsave");
        SceneManager.sceneLoaded += LoadState;
        Debug.Log("Resetload");
    }

    //load game
    public void LoadState(Scene s, LoadSceneMode mode)
    {
        //get path of saved data
        string path = Application.persistentDataPath + "/savefile.json";
        //check if exist
        if (File.Exists(path))
        {
            Debug.Log("Load state");
            //read content
            string json = File.ReadAllText(path);
            //transform into SaveData instance
            SaveData dataLoad = JsonUtility.FromJson<SaveData>(json);
            //set gameData refer SaveData
            //change player info
            userName = dataLoad.userName;
            //Debug.Log(userName);
            pesos = dataLoad.pesos;
            experience = dataLoad.experience;
            weapon.weaponLevel = dataLoad.weaponLevel;
            //set level of player
            if (GetCurrentLevel() != 1)
                player.SetLevel(GetCurrentLevel());
            player.transform.position = GameObject.Find("SpawnPoint").transform.position;
            //make call only once only
            SceneManager.sceneLoaded -= LoadState;
        }
        //----------------------------
        //check if has save data
        // if (!PlayerPrefs.HasKey("SaveState"))
        //     return;
        //string[] data = PlayerPrefs.GetString("SaveState").Split('|');
        // string[] data = "0|0|0|0".Split('|');
        //change player skin
        // pesos = int.Parse(data[1]);
        // experience = int.Parse(data[2]);
        // weapon.weaponLevel = int.Parse(data[3]);
        //set level of player
        // if (GetCurrentLevel() != 1)
        //     player.SetLevel(GetCurrentLevel());
        // player.transform.position = GameObject.Find("SpawnPoint").transform.position;
        //make call only once only
        // SceneManager.sceneLoaded -= LoadState;
        //-------------------------------
    }
    //on scene loaded - call every time load scene
    public void OnSceneLoaded(Scene s, LoadSceneMode mode)
    {
        //set spawn point
        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
    }
}
