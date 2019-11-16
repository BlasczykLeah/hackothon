using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cardComparer : MonoBehaviour
{
    public static cardComparer inst;

    public GameObject p1, p2;
    public Image p1Card, p2Card;
    Player1Me meCode;
    Player2AI aiCode;

    private void Awake()
    {
        if (inst != null) Destroy(this);
        else inst = this;
    }

    void Start()
    {
        p1Card = p2Card = null;
        meCode = p1.GetComponent<Player1Me>();
        aiCode = p2.GetComponent<Player2AI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(p1Card != null && p2Card != null)
        {
            // compare cards, destroy cards, reset, reactivate ai

                // compare, give points
            int battle = battleResults(p1Card.GetComponent<card>().cardType, p1Card.GetComponent<card>().cardType);
            if (battle == -1) aiCode.Points++;
            if (battle == 1) meCode.Points++;

                // reset stuffs
            battle = 0;
            p1Card = p2Card = null;
        }
    }

    int battleResults(CardType p1c, CardType p2c)
    {
        if(p1c == CardType.Wizard)
        {
            if(p2c == CardType.Archer)
            {
                //p1 wins
                return 1;
            }
            else if(p2c == CardType.Wizard)
            {
                //tie
                return 0;
            }
            else
            {
                //p2 wins
                return -1;
            }
        }
        else if(p1c == CardType.Knight)
        {
            if (p2c == CardType.Wizard)
            {
                //p1 wins
                return 1;
            }
            else if (p2c == CardType.Knight)
            {
                //tie
                return 0;
            }
            else
            {
                //p2 wins
                return -1;
            }
        }
        else if (p1c == CardType.Archer)
        {
            if (p2c == CardType.Knight)
            {
                //p1 wins
                return 1;
            }
            else if (p2c == CardType.Archer)
            {
                //tie
                return 0;
            }
            else
            {
                //p2 wins
                return -1;
            }
        }
        else if (p1c == CardType.Sovereign)
        {
            if (p2c == CardType.Dargon)
            {
                //p2 wins
                return -1;
            }
            else if (p2c == CardType.Sovereign)
            {
                //tie
                return 0;
            }
            else
            {
                //p1 wins
                return 1;
            }
        }
        else
        {
            if(p2c == CardType.Dargon)
            {
                // tie
                return 0;
            }
            else
            {
                // p1 wins
                return 1;
            }
        }
    }
}
