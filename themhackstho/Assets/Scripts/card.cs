using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class card : MonoBehaviour
{
    public CardType cardType;
    public bool isOP;
    public myDeck playerDeck;
    public int playerNum;

    public void chosenCard()
    {
        if (playerNum == 1) cardComparer.inst.p1Card = gameObject;
        else cardComparer.inst.p2Card = gameObject;

        cardComparer.inst.compare = true;
        playerDeck.myHand.Remove(gameObject.GetComponent<Image>());
    }
}

public enum CardType
{
    Wizard, Knight, Archer, Sovereign, Dargon
}
