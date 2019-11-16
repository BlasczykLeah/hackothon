using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player1Me : MonoBehaviour
{
    public int Points = 0, maxCards = 5;
    myDeck deck;
    public Image coin, coinSack;
    public List<Image> money;

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

    public void addCoin()
    {
        Image temp = Instantiate(coin, gameObject.transform);
        temp.transform.SetParent(coinSack.transform);
        money.Add(temp);
    }
}
