using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Alert : MonoBehaviour
{
    public Text alertText;

    public IEnumerator Show(string msg, int fontSize, Color color, float duration)
    {
        //change text in txt
        alertText.text = msg;
        alertText.fontSize = fontSize;
        alertText.color = color;
        //WorldToScreenPoint - transfer world space to screen space so can use in UI
        // transform.position = Camera.main.WorldToScreenPoint(position);
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }
}
