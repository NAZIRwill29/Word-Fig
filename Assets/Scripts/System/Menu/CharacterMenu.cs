using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    //text field
    public Text levelText, hitpointText, manapointText, pesosText, buyText, xpText, itemText;
    //logic
    private int currentCharacterSelection = 0;
    private int currentItemSelection = 0;
    public Image characterSelectionSprite, itemSelectionSprite;
    public RectTransform xpBar;

    //character selection
    public void OnArrowClick(bool right)
    {
        //if right button
        if (right)
        {
            currentCharacterSelection++;
            //if went too far
            if (currentCharacterSelection == GameManager.instance.playerSprites.Count)
                currentCharacterSelection = 0;
            OnSelectionChange();
        }
        else
        {
            currentCharacterSelection--;
            //if went too far
            if (currentCharacterSelection < 0)
                currentCharacterSelection = GameManager.instance.playerSprites.Count - 1;
            OnSelectionChange();
        }
    }

    //change char img
    private void OnSelectionChange()
    {
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
        GameManager.instance.player.SwapSprite(currentCharacterSelection);
    }

    //item selection
    public void OnArrowItemClick(bool right)
    {
        //if right button
        if (right)
        {
            currentItemSelection++;
            //if went too far
            if (currentItemSelection == GameManager.instance.itemSprites.Count)
                currentItemSelection = 0;
            OnSelectionItemChange();
        }
        else
        {
            currentItemSelection--;
            //if went too far
            if (currentItemSelection < 0)
                currentItemSelection = GameManager.instance.itemSprites.Count - 1;
            OnSelectionItemChange();
        }
    }

    //change item img and text when select
    private void OnSelectionItemChange()
    {
        itemSelectionSprite.sprite = GameManager.instance.itemSprites[currentItemSelection];
        buyText.text = GameManager.instance.itemPrices[currentItemSelection].ToString();
        itemText.text = GameManager.instance.itemNames[currentItemSelection].ToString();
    }

    //buy item
    public void BuyItem()
    {
        if (GameManager.instance.TryBuyPotion(currentItemSelection))
            UpdateMenu();
    }

    //upgrade char info -call from menubutton
    public void UpdateMenu()
    {
        //meta
        hitpointText.text = GameManager.instance.player.hitpoint.ToString() + " / " + GameManager.instance.player.maxHitpoint.ToString();
        manapointText.text = GameManager.instance.player.manapoint.ToString() + " / " + GameManager.instance.player.maxManapoint.ToString();
        pesosText.text = GameManager.instance.pesos.ToString();
        levelText.text = GameManager.instance.GetCurrentLevel().ToString();
        //xp Bar
        int currLevel = GameManager.instance.GetCurrentLevel();
        if (currLevel == GameManager.instance.xpTable.Count)
        {
            xpText.text = GameManager.instance.experience.ToString() + " total experience points";
            xpBar.localScale = Vector3.one;
        }
        else
        {
            int prevLevelXp = GameManager.instance.GetXpToLevel(currLevel - 1);
            int currLevelXp = GameManager.instance.GetXpToLevel(currLevel);
            int diff = currLevelXp - prevLevelXp;
            float currXpIntoLevel = GameManager.instance.experience - prevLevelXp;
            float completionRatio = (float)currXpIntoLevel / (float)diff;
            xpBar.localScale = new Vector3(completionRatio, 1, 1);
            xpText.text = currXpIntoLevel.ToString() + " / " + diff;
        }
    }
}
