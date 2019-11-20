using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player1Me : MonoBehaviour
{
    audioMan AM;
    public int Points = 0, maxCards = 5;
    myDeck deck;
    public Image coin, coinSack;
    public List<Image> money;

    void Start()
    {
        deck = GetComponent<myDeck>();
        AM = audioMan.instance;
    }

    void Update()
    {
        
    }

    public void drawCard()
    {
        if(deck.myHand.Count < 4 && Points > 0)
        {
            removeCoin();
            AM.draw();
            Points--;
            deck.pickCard();
            Invoke("addCardOffset", 0.5F);
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

    public void removeCoin()
    {
        Image temp = money[0];
        money.RemoveAt(0);
        Destroy(temp.gameObject);
    }

    void addCardOffset()
    {
        deck.pickCard();
    }
}
