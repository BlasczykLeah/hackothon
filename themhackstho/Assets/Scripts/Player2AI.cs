using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player2AI : MonoBehaviour
{
    public int Points = 0;
    public myDeck theDeck;
    public int[] playerChoices;

    void Start()
    {
        theDeck = GetComponent<myDeck>();

        // Wizard, Knight, Archer, Sovereign, Dargon
        playerChoices = new int[5] { 0, 0, 0, 0, 0 };
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

        return null;
    }

    public GameObject whatDoIDo()
    {
        if (theDeck.myHand.Count > 0)
        {
            int rand = Random.Range(0, 5);
            //if (rand < 2) return randomPick();
            //else return smartPick();
            return randomPick();
        }
        Debug.Log("I'm out of cards!");
        return null;
    }
}
