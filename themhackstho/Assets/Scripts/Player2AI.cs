using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player2AI : MonoBehaviour
{
    public int Points = 0;
    public myDeck theDeck;
    public int[] playerChoices;
    CardType[] cardTypes;

    void Start()
    {
        theDeck = GetComponent<myDeck>();

        // Wizard, Knight, Archer, Sovereign, Dargon
        playerChoices = new int[5] { 0, 0, 0, 0, 0 };
        cardTypes = new CardType[5] { CardType.Wizard, CardType.Knight, CardType.Archer, CardType.Sovereign, CardType.Dargon };
    }

    private void Update()
    {
        
    }

    public void updateChoices(CardType type)
    {
        if (type == CardType.Wizard) playerChoices[0]++;
        else if (type == CardType.Knight) playerChoices[1]++;
        else if (type == CardType.Archer) playerChoices[2]++;
        else if (type == CardType.Sovereign) playerChoices[3]++;
        else if (type == CardType.Dargon) playerChoices[4]++;
        else Debug.Log("welp");
    }

    public GameObject randomPick()
    {
        Debug.Log("IM RANDOMLY PICKING");
        int rand = Random.Range(0, theDeck.myHand.Count);
        GameObject theCard = theDeck.myHand[rand].gameObject;
        theDeck.myHand.RemoveAt(rand);
        Debug.Log("I picked " + theCard.GetComponent<card>().cardType.ToString());
        return theCard;
    }

    public GameObject smartPick()
    {
        Debug.Log("IM SMART PICKING");

        int[] priority = null;
        int index = 0, index2 = -1, index3 = -1, count = playerChoices[0];

        for(int i = 1; i < 3; i++)
        {
            if(playerChoices[i] == count)
            {
                if (index2 == -1) index2 = i;
                else index3 = i;
            } 
            else if(playerChoices[i] > count)
            {
                count = playerChoices[i];
                index = i;
                if (index2 != -1) index2 = -1;
            }
        }

        if(index3 != -1)
        {
            // doesnt matter, even odds
            Debug.Log("Its equal, just gonna random it then");
            return randomPick();
        }

        if (index2 != -1)
        {
            Debug.Log("Two are the same, they are: " + cardTypes[index].ToString() + " and " + cardTypes[index2].ToString());
            // picks: counter, specialS, specialD, tie, lose
            if (index == 0 && index2 == 1) priority = new int[5] { 0, 3, 4, 2, 1 };
            else if (index == 1 && index2 == 0) priority = new int[5] { 0, 3, 4, 2, 1 };

            else if (index == 1 && index2 == 2) priority = new int[5] { 1, 3, 4, 0, 2 };
            else if (index == 2 && index2 == 1) priority = new int[5] { 1, 3, 4, 0, 2 };

            else if (index == 0 && index2 == 2) priority = new int[5] { 2, 3, 4, 1, 0 };
            else if (index == 2 && index2 == 0) priority = new int[5] { 2, 3, 4, 1, 0 };

            else Debug.Log("sad");
        }

        else
        {
            Debug.Log("Only one is bigger, its: " + cardTypes[index].ToString());
            if (index == 0) priority = new int[5] { 2, 3, 4, 0, 1 };
            else if (index == 1) priority = new int[5] { 0, 3, 4, 1, 2 };
            else if (index == 2) priority = new int[5] { 1, 3, 4, 2, 0 };
            else Debug.Log("sad");
        }

        if (priority == null)
        {
            Debug.Log("Something broke D:");
            return null;
        }

        foreach (int a in priority) Debug.Log(a);

        // find cards
        for(int i = 0; i < 5; i++)
        {
            int myChoice = priority[i];
            Debug.Log("I wanna pick " + cardTypes[myChoice].ToString());
            foreach(Image cards in theDeck.myHand)
            {
                if(cards.GetComponent<card>().cardType == cardTypes[myChoice])
                {
                    Image mine = cards;
                    theDeck.myHand.Remove(cards);
                    Debug.Log("I picked " + cards.GetComponent<card>().cardType.ToString());
                    return mine.gameObject;
                }
            }
            Debug.Log("I don't have any " + cardTypes[i].ToString());
        }

        Debug.Log("Something broke D:");
        return null;
    }

    public GameObject whatDoIDo()
    {
        if (theDeck.myHand.Count > 0)
        {
            if(theDeck.myHand.Count == 1) return randomPick();
            return smartPick();
        }
        Debug.Log("I'm out of cards!");
        return null;
    }
}
