using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard : MonoBehaviour
{
    public GameObject charPrefab;
    protected GameObject wordObject;
    static int charNo = 0;
    protected GameObject charObj;
    protected GameObject targetChar;
    protected CanvasGroup keyboardCG;
    public char[] letters = new char[]
    {
        'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N',
        'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
    };

    // Start is called before the first frame update
    protected virtual void Start()
    {
        targetChar = GameObject.Find("Boss_3");
        charObj = GameObject.Find("CharObj");
        wordObject = GameObject.Find("Word");
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Space))
        if (Input.GetButtonDown("Jump"))
        {
            SpawnChar();
        }

    }

    //spawn char
    void SpawnChar()
    {
        if (charNo < 16)
        {
            //spawn object under parent
            //Instantiate(Object original, Vector3 position, Quaternion rotation, Transform parent)
            Instantiate(charPrefab, new Vector3(0, 0, 0), Quaternion.identity, transform);
            charNo += 1;
            Debug.Log(charNo);
            //create random letter
            int randomNo = Random.Range(0, letters.Length);
            //TODO - CHANGE PROPERTY OF CHAR ACCORDING TO RANDOM LETTER

        }
    }

    //make char shoot to target
    protected void CharToShoot()
    {
        charObj.GetComponent<CharObj>().ShootWord(targetChar);
    }

    //function after button click
    protected void ButtonClick()
    {
        charNo -= 1;
        Debug.Log(charNo);
        keyboardCG.interactable = false;
    }

    // protected void DisableClick()
    // {
    //     keyboardCG.interactable = false;
    // }

    // //function after CharObj hit obj
    // public void EnableClick()
    // {
    //     keyboardCG.interactable = true;
    // }
}
