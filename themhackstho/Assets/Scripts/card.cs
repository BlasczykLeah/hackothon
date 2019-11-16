using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class card : MonoBehaviour
{
    public CardType cardType;
    public myDeck playerDeck;

    public void chosenCard()
    {

    }
}

public enum CardType
{
    Wizard, Knight, Archer, Sovereign, Dargon
}
