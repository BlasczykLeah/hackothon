using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Me : MonoBehaviour
{
    public int Points = 0, maxCards = 5;
    myDeck deck;

    void Start()
    {
        deck = GetComponent<myDeck>();
    }

    void Update()
    {
        
    }

    public void drawCard()
    {
        if(deck.myHand.Count < 4)
        {
            deck.pickCard();
            deck.pickCard();
        }
        else
        {
            Debug.Log("HAND FULL");
        }
    }
}
