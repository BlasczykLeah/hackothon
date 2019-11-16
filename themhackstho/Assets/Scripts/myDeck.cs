using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class myDeck : MonoBehaviour
{
    public bool isPlayer;
    public Image hand;
    public Image[] cardPrefs;
    public int[] cardsLeft;
    public int cardCount = 0;

    //public List<Image> currentDeck;
    public List<Image> myHand;

    // Start is called before the first frame update
    void Start()
    {
            // Wizard, Knight, Archer, Sovereign, Dargon
        cardsLeft = new int[5] { 10, 10, 10, 2, 1 };
        foreach (int a in cardsLeft) cardCount += a;

        myHand = new List<Image>();

        for(int i = 0; i < 5; i++)
        {
            pickCard();
        }

        if (!isPlayer)
        {
            cardComparer.inst.p2Card = GetComponent<Player2AI>().randomPick();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void pickCard()
    {
        int randCard = Random.Range(0, cardCount);
        Image newCard = null;

        cardCount--;
        if (randCard < cardsLeft[0])    //picked wizard
        {
            //Debug.Log("Chose card 0.");
            cardsLeft[0]--;
            newCard = Instantiate(cardPrefs[0], hand.transform);
        }
        else if (randCard < cardsLeft[0] + cardsLeft[1])    // picked knight
        {
            //Debug.Log("Chose card 1.");
            cardsLeft[1]--;
            newCard = Instantiate(cardPrefs[1], hand.transform);
        }
        else if (randCard < cardsLeft[0] + cardsLeft[1] + cardsLeft[2])     // picked archer
        {
            //Debug.Log("Chose card 2.");
            cardsLeft[2]--;
            newCard = Instantiate(cardPrefs[2], hand.transform);
        }
        else if (randCard < cardsLeft[0] + cardsLeft[1] + cardsLeft[2] + cardsLeft[3])      // picked sov
        {
            //Debug.Log("Chose card 3.");
            cardsLeft[3]--;
            newCard = Instantiate(cardPrefs[3], hand.transform);
        }
        else if (randCard == cardCount)         // picked darg
        {
            //Debug.Log("Chose card 4.");
            cardsLeft[4]--;
            newCard = Instantiate(cardPrefs[4], hand.transform);
        }
        else
        {
            Debug.Log("I messed something up D: also dylan says hello :D");
            newCard = null;
        }

        newCard.transform.SetParent(hand.transform);
        if (isPlayer) newCard.GetComponent<card>().playerNum = 1;
        else newCard.GetComponent<card>().playerNum = 2;
        newCard.GetComponent<card>().playerDeck = this;

        myHand.Add(newCard);
    }
}
