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
    public Image coin, coinSack;
    public List<Image> money;

    void Start()
    {
        theDeck = GetComponent<myDeck>();

        // Wizard, Knight, Archer, Sovereign, Dargon
        playerChoices = new int[5] { 0, 0, 0, 0, 0 };
        cardTypes = new CardType[5] { CardType.Wizard, CardType.Knight, CardType.Archer, CardType.Sovereign, CardType.Dargon };
    }

    bool moreCardsMaybe(int cardCount)
    {
        if (Points < 1) return false;
        if (cardCount > 3) return false;

        int mostCopies = 1;
        if(cardCount == 3 || cardCount == 2)
        {
            if(cardCount == 3)
            {
                if (theDeck.myHand[0].GetComponent<card>().cardType == theDeck.myHand[1].GetComponent<card>().cardType && theDeck.myHand[0].GetComponent<card>().cardType == theDeck.myHand[1].GetComponent<card>().cardType) mostCopies = 3;
                else if (theDeck.myHand[0].GetComponent<card>().cardType == theDeck.myHand[1].GetComponent<card>().cardType) mostCopies = 2;
                else if (theDeck.myHand[0].GetComponent<card>().cardType == theDeck.myHand[2].GetComponent<card>().cardType) mostCopies = 2;
                else if (theDeck.myHand[1].GetComponent<card>().cardType == theDeck.myHand[2].GetComponent<card>().cardType) mostCopies = 2;
            }
            else
            {
                if (theDeck.myHand[0].GetComponent<card>().cardType == theDeck.myHand[1].GetComponent<card>().cardType) mostCopies = 2;
            }

            Debug.Log("Max copies: " + mostCopies);

            if (mostCopies == 1)
            {
                if (cardCount == 3)
                {
                    if (Points < 4) return false;
                    else
                    {
                        drawCard();
                        return true;
                    }
                }
                else
                {
                    if (Points < 3) return false;
                    else
                    {
                        drawCard();
                        return true;
                    }
                }
            }
            else if(mostCopies == 2)
            {
                if(cardCount == 3)
                {
                    if (Points < 3) return false;
                    else
                    {
                        drawCard();
                        return true;
                    }
                }
                else
                {
                    if (Points < 2) return false;
                    else
                    {
                        drawCard();
                        return true;
                    }
                }
            }
            else
            {
                if (Points < 3) return false;
                else
                {
                    drawCard();
                    return true;
                }
            }
        }
        else
        {
            if (Random.Range(0, 2) == 0)
            {
                drawCard();
                return true;
            }
            return false;
        }
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
            Debug.Log("I don't have any " + cardTypes[myChoice].ToString());
        }

        Debug.Log("Something broke D:");
        return null;
    }

    public GameObject whatDoIDo()
    {
        bool boughtStuff = moreCardsMaybe(theDeck.myHand.Count);

        if (theDeck.myHand.Count > 0)
        {
            if (!boughtStuff) cardComparer.inst.P1Turn();
            if(theDeck.myHand.Count == 1) return randomPick();
            return smartPick();
        }
        Debug.Log("I'm out of cards!");
        if (!drawCard()) return null;
        else return smartPick();
    }

    public bool drawCard()
    {
        Debug.Log("Imma buy some cards");
        if (theDeck.myHand.Count < 4 && Points > 0)
        {
            removeCoin();
            Points--;

            theDeck.aiAddCard();
            theDeck.pickCard();
            Invoke("addCard", 0.5F);

            return true;
        }
        else
        {
            Debug.Log("nvm");
            return false;
        }
    }

    public void AIaddCoin()
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

    void addCard()
    {
        theDeck.pickCard();
        theDeck.aiAddCard();
        cardComparer.inst.P1Turn();
    }
}
