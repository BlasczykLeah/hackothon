using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class myDeck : MonoBehaviour
{
    public Image[] cardPrefs;
    public int[] cardsLeft;
    public int cardCount = 0;

    //public List<Image> currentDeck;
    public List<Image> myHand;

    // Start is called before the first frame update
    void Start()
    {
            // Wizard, Knight, Archer, Sovereign, Dargon
        cardsLeft = new int[5] { 10, 10, 10, 1, 1 };
        foreach (int a in cardsLeft) cardCount += a;

        myHand = new List<Image>();

        for(int i = 0; i < 5; i++)
        {
            int randCard = Random.Range(0, cardCount);
            Debug.Log("Num: " + randCard);
            myHand.Add(pickCard(randCard));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Image pickCard(int index)
    {
        cardCount--;
        if (index < cardsLeft[0])
        {
            Debug.Log("Chose card 0.");
            cardsLeft[0]--;
            return cardPrefs[0];
        }
        else if (index < cardsLeft[0] + cardsLeft[1])
        {
            Debug.Log("Chose card 1.");
            cardsLeft[1]--;
            return cardPrefs[1];
        }
        else if (index < cardsLeft[0] + cardsLeft[1] + cardsLeft[2])
        {
            Debug.Log("Chose card 2.");
            cardsLeft[2]--;
            return cardPrefs[2];
        }
        else if (index < cardsLeft[0] + cardsLeft[1] + cardsLeft[2] + cardsLeft[3])
        {
            Debug.Log("Chose card 3.");
            cardsLeft[3]--;
            return cardPrefs[3];
        }
        else if (index == cardCount)
        {
            Debug.Log("Chose card 4.");
            cardsLeft[4]--;
            return cardPrefs[4];
        }
        else
        {
            Debug.Log("I messed something up D:");
            return null;
        }
    }
}
