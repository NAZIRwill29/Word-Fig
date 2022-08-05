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
    public List<Sprite> itemSprites;
    public List<int> itemPrices;
    public List<string> itemNames;
    public List<int> xpTable;
    public Player player;
    public FloatingTextManager floatingTextManager;
    // public Weapon weapon;
    //logic
    public int pesos;
    public int experience;
    public RectTransform hitpointBar;
    public RectTransform manapointBar;
    public Text hitpointText;
    public Text manapointText;
    public GameObject canvas;
    public Animator deathMenuAnim;
    public Text infoLevelText;
    public Keyboard keyboardScript;
    public GameObject dontDestroyGameObject;
    public MainMenuController mainMenuControllerScript;
    public GameObject[] startButtons;
    private string spawnPointName = "SpawnPoint1";
    [SerializeField]
    private int stagePassedNo;
    // public InputField userNameInput;
    // public Text userNameText;
    // public string userName;
    // private string dataSave;

    //required for JsonUtility
    //it will only transform things to JSON if they are tagged as Serializable.
    [System.Serializable]
    class SaveData
    {
        // public string userName;
        public int pesos;
        public int experience;
        public int level;
        public int damage;
        public int stage;
    }

    private void Awake()
    {
        //check if have gameObject
        if (GameManager.instance != null)
        {
            StartCoroutine(SetStage(true));
            //set stage based on how many stage passed
            Debug.Log("awake1");
            return;
        }
        instance = this;
        SceneManager.sceneLoaded += LoadState;
        SceneManager.sceneLoaded += OnSceneLoaded;
        Debug.Log("awake2");
        StartCoroutine(SetStage(false));
        //userNameInput.GetComponentInChildren<Text>().text = userName;
    }

    private IEnumerator SetStage(bool isInstance)
    {
        if (isInstance)
        {
            //set stage if instance != null
            yield return StartCoroutine(TurnOnStageButton());
            yield return StartCoroutine(DestroyGameObject());
        }
        else
        {
            //set stage if instance = null 
            //- turn on stage button based on player achievement
            yield return StartCoroutine(LoadData());
            yield return StartCoroutine(TurnOnStageButton());
        }

    }

    //coroutine for setStage
    private IEnumerator DestroyGameObject()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
        Destroy(dontDestroyGameObject);
        Destroy(canvas);
        Destroy(mainMenuControllerScript.gameObject);
    }
    private IEnumerator LoadData()
    {
        yield return new WaitForSeconds(0);
        //Debug.Log("load stageNo = " + stagePassedNo);
        SceneManager.sceneLoaded += LoadState;
    }
    private IEnumerator TurnOnStageButton()
    {
        yield return new WaitForSeconds(0);
        //Debug.Log("set stage = " + GameManager.instance.stagePassedNo);
        TurnStageButton(true, 0, GameManager.instance.stagePassedNo + 1);
    }

    //stageButton - turn on/off
    private void TurnStageButton(bool isWantTurnOn, int min, int max)
    {
        Debug.Log(max);
        for (int i = min; i < max; i++)
        {
            //turn on/off startButton
            GameManager.instance.startButtons[i].GetComponent<CanvasGroup>().interactable = isWantTurnOn;
        }
    }

    //when on game turn on player and canvas and mainmneuUi
    public void OnStartGame()
    {
        SceneManager.sceneLoaded += LoadState;
        // player.gameObject.SetActive(true);
        canvas.SetActive(true);
        mainMenuControllerScript.gameObject.SetActive(false);
        // userName = userNameInput.text;
        // userNameText.text = userName;
    }
    //when on MainMenu scene turn off player and canvas and mainmenuUI
    public void OnMainMenu()
    {
        // player.gameObject.SetActive(false);
        canvas.SetActive(false);
        mainMenuControllerScript.gameObject.SetActive(true);
    }

    //to make as reference call function in floatingtextmanager script
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    //decision for try buy hp / mp potion
    public bool TryBuyPotion(int itemSelection)
    {
        //enough money
        if (pesos >= itemPrices[itemSelection])
        {
            pesos -= itemPrices[itemSelection];
            Debug.Log("buy item");
            //check potion type
            switch (itemSelection)
            {
                //small hp potion
                case 0:
                    player.Heal(10, true);
                    break;
                //small mp potion
                case 1:
                    player.Heal(10, false);
                    break;
                //small exp potion
                case 2:
                    GrantXp(10);
                    break;
                //big hp potion
                case 3:
                    player.Heal(40, true);
                    break;
                //big mp potion
                case 4:
                    player.Heal(40, false);
                    break;
                //big exp potion
                case 5:
                    GrantXp(40);
                    break;
                default:
                    break;
            }
            return true;
        }
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
        //fill in info
        infoLevelText.text = "Lv " + r;
        //set leyer player
        player.SetLevelPlayer(r);
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
        //for raise hp mp after level up 
        OnHitpointChange();
        OnManapointChange();
        //set leveltext at info
        infoLevelText.text = "Lv " + GetCurrentLevel().ToString();
    }
    //hitpoint bar
    public void OnHitpointChange()
    {
        float ratio = (float)player.hitpoint / (float)player.maxHitpoint;
        hitpointBar.localScale = new Vector3(ratio, 1, 1);
        hitpointText.text = player.hitpoint.ToString() + " / " + player.maxHitpoint.ToString();
    }
    //mana bar
    public void OnManapointChange()
    {
        float ratio = (float)player.manapoint / (float)player.maxManapoint;
        manapointBar.localScale = new Vector3(ratio, 1, 1);
        manapointText.text = player.manapoint.ToString() + " / " + player.maxManapoint.ToString();
    }
    //death menu and respawn
    public void Respawn()
    {
        deathMenuAnim.SetTrigger("Hide");
        UnityEngine.SceneManagement.SceneManager.LoadScene(mainMenuControllerScript.sceneName);
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
        // data.userName = userName;
        data.pesos = pesos;
        data.experience = experience;
        data.level = player.levelPlayer;
        data.damage = player.damage;
        data.stage = stagePassedNo;
        //transform instance to json
        string json = JsonUtility.ToJson(data);
        //method to write string to a file
        /*Application.persistentDataPath - give you a folder where you can save data that 
        will survive between application reinstall or update and append to it the filename savefile.json*/
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        Debug.Log("Save state");
        Debug.Log("stage = " + data.stage);
    }

    public void ResetSaveState()
    {
        pesos = 0;
        experience = 0;
        player.SetLevel(1);
        player.SetDamagePlayer(1);
        stagePassedNo = 0;
        // userName = "";
        SaveState();
        Debug.Log("Resetsave");
        SceneManager.sceneLoaded += LoadState;
        Debug.Log("Resetload");
        TurnStageButton(false, 1, startButtons.Length);
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
            // userName = dataLoad.userName;
            //Debug.Log(userName);
            pesos = dataLoad.pesos;
            experience = dataLoad.experience;
            player.SetLevel(dataLoad.level);
            player.SetDamagePlayer(dataLoad.damage);
            stagePassedNo = dataLoad.stage;
            //set level of player
            if (GetCurrentLevel() != 1)
                player.SetLevel(GetCurrentLevel());
            try
            {
                //check if exist
                if (GameObject.Find("SpawnPoint1"))
                {
                    player.transform.position = GameObject.Find("SpawnPoint1").transform.position;
                }
            }
            catch (System.Exception e)
            {
                //handle error
                Debug.Log(e.Message);
            }
            OnHitpointChange();
            OnManapointChange();
            player.SetDamage(GetCurrentLevel());
            //make call only once only
            SceneManager.sceneLoaded -= LoadState;
            Debug.Log("stage = " + stagePassedNo);
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
        try
        {
            //check if exist
            if (GameObject.Find(spawnPointName))
            {
                //set spawn point
                player.transform.position = GameObject.Find(spawnPointName).transform.position;
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
        OnHitpointChange();
        OnManapointChange();
    }

    //get spawnPointName from portal
    public void PassSpawnPointName(string name)
    {
        spawnPointName = name;
    }

    //get stage passed by player
    public void PassStagePassed(int num)
    {
        //prevent from decreased
        if (stagePassedNo < num)
            stagePassedNo = num;
    }

    //Back to MainMenuScene
    public void BackToMenu()
    {
        //set spawnPoint to be same position as when player left
        //spawnPointName = "SpawnPoint";
        // string name = SceneManager.GetActiveScene().name;
        // Debug.Log(name);
        // mainMenuControllerScript.PassSceneName(name);
        GameObject spawnPoint = GameObject.Find(spawnPointName);
        spawnPoint.transform.position = player.transform.position;
        OnMainMenu();
        SaveState();
        SceneManager.LoadScene(0);
    }
}
