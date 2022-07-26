using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempChar : MonoBehaviour
{
    public int noCharInWord = 0;
    //add char to word
    public void AddToWord(char letter)
    {
        if (noCharInWord < 10)
        {
            Char charScript = gameObject.GetComponentInChildren<Char>();
            charScript.CharAddToWord(letter);
            noCharInWord += 1;
            Debug.Log(noCharInWord);
        }
    }

    //delete word if not collide with enemy
    public void DeleteChar()
    {
        Char charScript = gameObject.GetComponentInChildren<Char>();
        charScript.DestroyChar();
    }
}
