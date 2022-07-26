using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard : MonoBehaviour
{
    public GameObject charPrefab;
    protected GameObject wordObject;
    protected GameObject tempChar;
    private int charNo = 0;
    protected GameObject charObj;
    protected GameObject targetChar;
    protected CanvasGroup keyboardCG;
    public char letterClick;
    public char[] letters = new char[]
    {
        'A', 'A', 'A', 'A', 'B', 'C', 'D', 'E', 'E', 'E', 'E', 'F', 'G', 'H', 'I', 'I', 'I', 'I', 'J', 'K', 'L', 'M', 'N',
        'O', 'O', 'O', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'U', 'U', 'U', 'V', 'W', 'X', 'Y', 'Z'
    };

    // Start is called before the first frame update
    protected virtual void Start()
    {
        targetChar = GameObject.Find("Boss_3");
        charObj = GameObject.Find("CharObj");
        wordObject = GameObject.Find("Word");
        tempChar = GameObject.Find("TempChar");
    }

    // Update is called once per frame
    void FixedUpdate()
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

        }
    }

    //make char shoot to target and pass letter
    protected void CharToShoot(char letter)
    {
        Debug.Log("letterClick = " + letter);
        charObj.GetComponent<CharObj>().ShootWord(targetChar, letter);
    }

    //function after button click
    protected void ButtonClick()
    {
        charNo -= 1;
        Debug.Log(charNo);
        keyboardCG.interactable = false;
    }

    //create random letter
    protected char RandomLetter()
    {
        int randomNo = Random.Range(0, letters.Length);
        return letters[randomNo];
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
