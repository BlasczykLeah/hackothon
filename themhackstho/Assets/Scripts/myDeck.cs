using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class myDeck : MonoBehaviour
{
    public bool isPlayer, outOfCards;
    public Image hand, aiHand;
    public Text countTxt;
    public GameObject deckBtn;
    public Image[] cardPrefs;
    public Image[] cardPrefsOP;
    public int[] cardsLeft;
    public int cardCount = 0;
    bool opArch = false, opWiz = false, opKni = false;

    //public List<Image> currentDeck;
    public List<Image> myHand;
    List<Image> aiCards;

    // Start is called before the first frame update
    void Start()
    {
            // Wizard, Knight, Archer, Sovereign, Dargon
        cardsLeft = new int[5] { 10, 10, 10, 2, 1 };
        foreach (int a in cardsLeft) cardCount += a;

        myHand = new List<Image>();
        if (!isPlayer) aiCards = new List<Image>();

        for(int i = 0; i < 5; i++)
        {
            pickCard();
            if (!isPlayer)
            {
                Image temp = Instantiate(cardPrefs[5], aiHand.transform);
                temp.transform.SetParent(aiHand.transform);
                aiCards.Add(temp);
            }
        }

        if (!isPlayer)
        {
            cardComparer.inst.p2Card = GetComponent<Player2AI>().randomPick();
        }
    }

    public void pickCard()
    {
        if (cardCount <= 0) return;
        int randCard = Random.Range(0, cardCount);
        Image newCard = null;
        countTxt.text = "Cards Left: " + cardCount;

        cardCount--;
        if (cardCount <= 0)
        {
            outOfCards = true;
            deckBtn.SetActive(false);
        }

        bool pickedOp = false;
        if (randCard < cardsLeft[0])    //picked wizard
        {
            if (cardsLeft[0] == 1 && !opWiz) pickedOp = true;
            else if(!opWiz)
            {
                int rand = Random.Range(0, cardsLeft[0]);
                if (rand == 0) pickedOp = true;
            }
            cardsLeft[0]--;
            if (!pickedOp) newCard = Instantiate(cardPrefs[0], hand.transform);
            else
            {
                opWiz = true;
                newCard = Instantiate(cardPrefsOP[0], hand.transform);
            }
        }

        else if (randCard < cardsLeft[0] + cardsLeft[1])    // picked knight
        {
            if (cardsLeft[1] == 1 && !opKni) pickedOp = true;
            else if(!opKni)
            {
                int rand = Random.Range(0, cardsLeft[1]);
                if (rand == 0) pickedOp = true;
            }
            cardsLeft[1]--;
            if (!pickedOp) newCard = Instantiate(cardPrefs[1], hand.transform);
            else
            {
                opKni = true;
                newCard = Instantiate(cardPrefsOP[1], hand.transform);
            }
        }

        else if (randCard < cardsLeft[0] + cardsLeft[1] + cardsLeft[2])     // picked archer
        {
            if (cardsLeft[2] == 1 && !opArch) pickedOp = true;
            else if(!opArch)
            {
                int rand = Random.Range(0, cardsLeft[2]);
                if (rand == 0) pickedOp = true;
            }
            cardsLeft[2]--;
            if (!pickedOp) newCard = Instantiate(cardPrefs[2], hand.transform);
            else
            {
                opArch = true;
                newCard = Instantiate(cardPrefsOP[2], hand.transform);
            }
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

    public void aiAddCard()
    {
        Image temp = Instantiate(cardPrefs[5], aiHand.transform);
        temp.transform.SetParent(aiHand.transform);
        aiCards.Add(temp);
    }

    public void aiSubCard()
    {
        Image temp = aiCards[0];
        aiCards.RemoveAt(0);
        Destroy(temp.gameObject);
    }

    public void giveCard(myDeck otherDeck)
    {
        Image card;
        Debug.Log("I have " + otherDeck.myHand.Count + " cards to choose from");

        if (otherDeck.myHand.Count == 0) return;
        else if(otherDeck.myHand.Count == 1)
        {
            card = otherDeck.myHand[0];
        }
        else
        {
            int rand = Random.Range(0, otherDeck.myHand.Count);
            card = otherDeck.myHand[rand];
        }

        Debug.Log("I took a " + card.GetComponent<card>().cardType.ToString());
        otherDeck.myHand.Remove(card);
        myHand.Add(card);
        card.GetComponent<card>().playerDeck = this;
        if (isPlayer) card.GetComponent<card>().playerNum = 1;
        else card.GetComponent<card>().playerNum = 2;

        card.transform.SetParent(hand.transform);
        if (!isPlayer)
        {
            aiAddCard();
            card.transform.position = new Vector3(1000, 1000, 1000);
        }
        else
        {
            otherDeck.aiSubCard();
        }
    }
}
