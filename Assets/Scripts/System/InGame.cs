using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame : MonoBehaviour
{
    [Tooltip("stage no to set when cleared")]
    [SerializeField]
    private int stageNo;
    [SerializeField]
    private string stageName;
    private MainMenuController mainMenuControllerScript;
    private AudioSource inGameAudio;
    public GameObject boss, bossHp;
    //clearStageSound
    [Tooltip("clearStageSound")]
    public AudioClip[] inGameSound;
    public BackgroundMusic backgroundMusicScript;
    void Awake()
    {
        mainMenuControllerScript = GameManager.instance.mainMenuControllerScript;
        //pass stage data
        mainMenuControllerScript.PassSceneName(stageName);
    }
    // Start is called before the first frame update
    void Start()
    {
        inGameAudio = GetComponent<AudioSource>();
        PreventResapwnBoss();
        Debug.Log("prevent");
    }

    // Update is called once per frame
    void Update()
    {

    }

    //pass stageNo to when clear stage
    public void PassStagePassed()
    {
        //pass stageNo to gameManager
        GameManager.instance.PassStagePassed(stageNo);
        inGameAudio.PlayOneShot(inGameSound[0], 1.0f);
        backgroundMusicScript.ClearStageMusic();
        GameManager.instance.SetPreventSpawnBoss(true);
    }

    //prevent boss from respawn
    private void PreventResapwnBoss()
    {
        //check preventspawnBoss, boss exist, bossHp exist
        if (GameManager.instance.preventSpawnBoss && boss && bossHp)
        {
            boss.SetActive(false);
            bossHp.SetActive(false);
        }
    }
}
