using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class myDeck : MonoBehaviour
{
    public List<Image> currentDeck;
    public List<Image> myHand;

    // Start is called before the first frame update
    void Start()
    {
        currentDeck = new List<Image>();
        myHand = new List<Image>();

        for(int i = 0; i < 5; i++)
        {
            int randCard = Random.Range(0, currentDeck.Count);
            myHand.Add(currentDeck[randCard]);
            currentDeck.Remove(currentDeck[randCard]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
