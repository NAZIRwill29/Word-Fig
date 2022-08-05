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
    void Awake()
    {
        mainMenuControllerScript = GameManager.instance.mainMenuControllerScript;
        //pass stage data
        mainMenuControllerScript.PassSceneName(stageName);
    }
    // Start is called before the first frame update
    void Start()
    {

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
    }
}
