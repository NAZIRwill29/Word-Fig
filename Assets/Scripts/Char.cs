using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Char : MonoBehaviour
{

    private bool isClick;
    public char letter;
    public GameObject keyboard;
    [SerializeField]
    public GameObject image;
    private Image imageImg;
    private Keyboard keyboardScript;
    private Word wordScript;
    private string special;
    // Start is called before the first frame update
    void Start()
    {
        keyboardScript = keyboard.GetComponent<Keyboard>();
        wordScript = keyboardScript.wordObject.GetComponent<Word>();
        imageImg = GetComponent<Image>();
    }

    //action after button char click
    public void CharButtonClick()
    {
        if (!isClick)
        {
            //function button in keyboard
            if (wordScript.totalCharInWord < 8)
            {
                keyboardScript.ButtonClick();
                //shoot charObj
                keyboardScript.CharToShoot(letter, special);
                keyboardScript.charObj.GetComponent<CharObj>().charAnim.SetTrigger("show");
                AddToTemp();
            }
        }
        else
        {
            //function button in wordbox
            AddToBirthchar(true);
        }
    }

    //add to keyboard
    public void AddToKeyboard()
    {
        WriteLetter();
        transform.SetParent(keyboard.transform, true);
    }

    //add to temp
    private void AddToTemp()
    {
        transform.SetParent(keyboardScript.tempChar.transform, true);
    }

    //add to word
    public void AddToWord(char letter)
    {
        isClick = true;
        WriteLetter(letter);
        transform.SetParent(keyboardScript.wordObject.transform, true);
        //turn off special char image
        image.SetActive(false);
        //for limit char in word
        wordScript.ChangeTotalCharInWord(1);
    }

    //add to birthChar
    public void AddToBirthchar(bool isParentWord)
    {
        transform.SetParent(keyboardScript.birthChar.transform, true);
        //turn on special char image
        image.SetActive(true);
        //refresh isClick 
        isClick = false;
        if (isParentWord)
            wordScript.ChangeTotalCharInWord(-1);
    }

    //write random letter
    public void WriteLetter()
    {
        letter = keyboardScript.RandomLetter();
        // Debug.Log("LetterChar = " + letter);
        gameObject.GetComponentInChildren<Text>().text = letter.ToString();
    }
    //write letter
    public void WriteLetter(char letter)
    {
        // Debug.Log("LetterCharWord = " + letter);
        gameObject.GetComponentInChildren<Text>().text = letter.ToString();
    }

    //set special char
    public void SetSpecialChar(string text)
    {
        ChangeSpecialCharImage(text);
        special = text;
    }

    //change source image for specialChar - only when level up
    public void ChangeSpecialCharImage(string text)
    {
        switch (text)
        {
            case "thunder":
                imageImg.sprite = keyboardScript.specialCharSprites[0];
                break;
            case "ice":
                imageImg.sprite = keyboardScript.specialCharSprites[1];
                break;
            case "fire":
                imageImg.sprite = keyboardScript.specialCharSprites[2];
                break;
            case "wind":
                imageImg.sprite = keyboardScript.specialCharSprites[3];
                break;
            default:
                break;
        }
    }
}
