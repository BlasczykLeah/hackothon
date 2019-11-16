using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2AI : MonoBehaviour
{
    public int Points = 0;
    public myDeck theDeck;

    void Start()
    {
        theDeck = GetComponent<myDeck>();
    }

    public GameObject randomPick()
    {
        if(theDeck.myHand.Count > 0)
        {
            int rand = Random.Range(0, theDeck.myHand.Count);
            GameObject theCard = theDeck.myHand[rand].gameObject;
            theDeck.myHand.RemoveAt(rand);
            Debug.Log("I picked " + theCard.GetComponent<card>().cardType.ToString());
            return theCard;
        }
        Debug.Log("I'm out of cards!");
        return null;
    }
}
