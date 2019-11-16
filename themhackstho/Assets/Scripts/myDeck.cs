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

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
