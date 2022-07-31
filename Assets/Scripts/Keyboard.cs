using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard : MonoBehaviour
{
    public GameObject charPrefab;
    public GameObject wordObject;
    public GameObject tempChar;
    public GameObject charObj;
    public GameObject targetChar;
    private CanvasGroup keyboardCG;
    public char letterClick;
    public GameObject birthChar;
    private BirthChar birthCharScript;
    public Sprite[] specialCharSprites;
    private char[] letters = new char[]
    {
        'A', 'A', 'A', 'A', 'A', 'A', 'A',
        'B', 'B', 'B',
        'C', 'C', 'C',
        'D', 'D', 'D',
        'E', 'E', 'E', 'E', 'E', 'E', 'E',
        'F', 'F', 'F',
        'G', 'G',
        'H', 'H', 'H',
        'I', 'I', 'I', 'I', 'I', 'I', 'I',
        'J', 'J',
        'K', 'K', 'K',
        'L', 'L', 'L',
        'M', 'M', 'M',
        'N', 'N', 'N',
        'O', 'O', 'O', 'O', 'O', 'O', 'O',
        'P', 'P',
        'Q',
        'R', 'R',
        'S', 'S',
        'T',
        'U', 'U', 'U', 'U', 'U', 'U', 'U',
        'V',
        'W', 'W',
        'X',
        'Y', 'Y', 'Y',
        'Z'
    };

    // Start is called before the first frame update
    void Start()
    {
        keyboardCG = GetComponent<CanvasGroup>();
        birthCharScript = birthChar.GetComponent<BirthChar>();
        //charObj = GameObject.Find("CharObj");
        // wordObject = GameObject.Find("Word");
        // tempChar = GameObject.Find("TempChar");
    }

    // Update is called once per frame
    void Update()
    {
    }

    //make char shoot to target and pass letter
    public void CharToShoot(char letter, string specialText)
    {
        // Debug.Log("letterClick = " + letter);
        charObj.GetComponent<CharObj>().ShootWord(targetChar, letter, specialText);
    }

    //function after button click
    public void ButtonClick()
    {
        keyboardCG.interactable = false;
        keyboardCG.alpha = 0.7f;
        birthCharScript.MinusCharNoInKeyboard();
    }

    //create random letter
    public char RandomLetter()
    {
        int randomNo = Random.Range(0, letters.Length);
        return letters[randomNo];
    }

    //set special char
    public void SetSpecialChar(int level)
    {
        birthCharScript.SetSpecialChar(level);
    }
}
