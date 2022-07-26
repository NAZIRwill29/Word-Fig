using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Char : Keyboard
{

    private bool isClick;
    private char letter;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        keyboardCG = GetComponentInParent<CanvasGroup>();
        WriteLetter();
    }

    //action after button char click
    public void CharButtonClick()
    {
        if (!isClick)
        {
            ButtonClick();
            CharToShoot(letter);
            AddToTemp();
            // Destroy(gameObject);
            // AddToWord();
        }
        // else
        // {
        //     Destroy(gameObject);
        // }
    }

    //TODO
    //MAKE WORD- COMBINE letter, harm enemy

    //add to temp
    private void AddToTemp()
    {
        transform.SetParent(tempChar.transform, true);
    }

    //add to word
    public void CharAddToWord(char letter)
    {
        isClick = true;
        Debug.Log("click");
        WriteLetter(letter);
        transform.SetParent(wordObject.transform, true);
    }

    //destroy char
    public void DestroyChar()
    {
        Destroy(gameObject);
    }

    //write random letter
    public void WriteLetter()
    {
        letter = RandomLetter();
        Debug.Log("LetterChar = " + letter);
        gameObject.GetComponentInChildren<Text>().text = letter.ToString();
    }
    //write letter
    public void WriteLetter(char letter)
    {
        Debug.Log("LetterCharWord = " + letter);
        gameObject.GetComponentInChildren<Text>().text = letter.ToString();
    }

}
