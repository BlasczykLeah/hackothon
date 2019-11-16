using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cardComparer : MonoBehaviour
{
    public static cardComparer inst;

    public bool compare = false;
    public GameObject p1, p2;
    public GameObject p1Card, p2Card;
    public GameObject dropzone;
    Player1Me meCode;
    Player2AI aiCode;

    GameObject winnerCard;

    public int battle;

    [Header("Points")]
    public int P1Points;
    public int P2Points;

    private void Awake()
    {
        if (inst != null) Destroy(this);
        else inst = this;
    }

    void Start()
    {
        //p1Card = p2Card = null;
        meCode = p1.GetComponent<Player1Me>();
        aiCode = p2.GetComponent<Player2AI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(p1Card != null && p2Card != null && compare)
        {
            p2Card.transform.SetParent(dropzone.transform);
            p2.GetComponent<myDeck>().aiSubCard();

            Debug.Log("Imma compare in a sec");
            Invoke("theBattle", 1F);
            compare = false;
        }

        P1Points = meCode.Points;
        P2Points = aiCode.Points;
    }

    int battleResults(CardType p1c, CardType p2c)
    {
        //Debug.Log("Comparing: " + p1c.ToString() + " + " + p2c.ToString());
        aiCode.updateChoices(p1c);

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

    void theBattle()
    {
        // compare, give points
        battle = battleResults(p1Card.GetComponent<card>().cardType, p2Card.GetComponent<card>().cardType);
        if (battle == -1)
        {
            Debug.Log("P2 WON BATTLE");
            Destroy(p1Card);
            winnerCard = p2Card;
            aiCode.Points++;
        }
        else if (battle == 1)
        {
            Debug.Log("P1 WON BATTLE");
            Destroy(p2Card);
            meCode.addCoin();
            winnerCard = p1Card;
            meCode.Points++;
        }
        else
        {
            Debug.Log("TIE");
            winnerCard = null;
        }
        if (checkForWin(meCode.Points, aiCode.Points)) ; //somebody wins
        else Invoke("resetStuffs", 2F);
    }

    void resetStuffs()
    {
        // reset stuffs & destroy cards
        battle = 0;
        dropzone.GetComponent<DropZone>().placed = false;
        if (winnerCard != null) Destroy(winnerCard);
        else
        {
            Destroy(p1Card);
            Destroy(p2Card);
        }
        p1Card = p2Card =  winnerCard = null;

        // new p2 move
        p2Card = aiCode.whatDoIDo();
    }

    bool checkForWin(int p1Score, int p2Score)
    {
        if (p1Score < 5 && p2Score < 5) return false;
        else
        {
            foreach (GameObject cards in GameObject.FindGameObjectsWithTag("Card")) Destroy(cards);
            // disable ability to buy things, dunno other win things
            if (p1Score >= 5)
            {
                // P1 wins, do stuffs
                Debug.Log("P1 Wins!");
            }
            else
            {
                // P2 wins, od stuffs
                Debug.Log("P2 Wins!");
            }

            return true;
        }
    }
}
