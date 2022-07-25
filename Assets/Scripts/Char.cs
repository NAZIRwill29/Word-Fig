using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char : Keyboard
{

    private bool isClick;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        keyboardCG = GetComponentInParent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //action after button char click
    public void CharButtonClick()
    {
        if (!isClick)
        {
            CharToShoot();
            ButtonClick();
            //Destroy(gameObject);
            AddToWord();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //add to word box
    private void AddToWord()
    {
        //todo
        gameObject.SetActive(false);
        transform.SetParent(wordObject.transform, true);
        isClick = true;
        Debug.Log("click");
    }

    //write letter
    public void WriteLetter()
    {
    }
}
