using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cardComparer : MonoBehaviour
{
    public static cardComparer inst;

    audioMan AM;

    public bool compare = false, eagleEye = false;
    public GameObject p1, p2;
    public GameObject p1Card, p2Card;
    public GameObject dropzone, endScreen;
    Player1Me meCode;
    Player2AI aiCode;
    public Text winner;

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
        AM = audioMan.instance;
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

    int battleResults(CardType p1c, CardType p2c, bool isOP1, bool isOP2)
    {
        aiCode.updateChoices(p1c);

        // compare op cards
        /*  -fire knight can kill the dragon
         *  -arcane trickster ...
         *  -eagle-eye archer can see opponents next card
         */
            // fire knight
        if (p1c == CardType.Knight && isOP1 && p2c == CardType.Dargon) return 1; 
        if (p2c == CardType.Knight && isOP2 && p1c == CardType.Dargon) return -1;

            // eagle-eye archer
        //if (p1c == CardType.Archer && isOP1) eagleEye = true;

        if(p1c == CardType.Wizard)
        {
            if(p2c == CardType.Archer)
            {
                if (isOP1)
                {
                    Debug.Log("P1 takes a card");
                    p1.GetComponent<myDeck>().giveCard(p2.GetComponent<myDeck>());
                }

                //p1 wins
                return 1;
            }
            else if(p2c == CardType.Wizard)
            {
                // op wizard things
                if (isOP1 && isOP2) return 0;
                else if (isOP1)
                {
                    Debug.Log("P1 takes a card");
                    p1.GetComponent<myDeck>().giveCard(p2.GetComponent<myDeck>());
                    return 1;
                }
                else if (isOP2)
                {
                    Debug.Log("P2 takes a card");
                    p2.GetComponent<myDeck>().giveCard(p1.GetComponent<myDeck>());
                    return -1;
                }
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
                if (isOP1 && isOP2) return 0;
                else if (isOP1) return 1;
                else if (isOP2) return -1;
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
                if(isOP1) eagleEye = true;
                return 1;
            }
            else if (p2c == CardType.Archer)
            {
                if (isOP1 && isOP2) return 0;
                else if (isOP1)
                {
                    eagleEye = true;
                    return 1;
                }
                else if (isOP2) return -1;
                //tie
                return 0;
            }
            else
            {
                if (isOP2)
                {
                    Debug.Log("P2 takes a card");
                    p2.GetComponent<myDeck>().giveCard(p1.GetComponent<myDeck>());
                }

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
        battle = battleResults(p1Card.GetComponent<card>().cardType, p2Card.GetComponent<card>().cardType, p1Card.GetComponent<card>().isOP, p2Card.GetComponent<card>().isOP);
        if (battle == -1)
        {
            Debug.Log("P2 WON BATTLE");
            AM.Defeat();
            Destroy(p1Card);
            aiCode.AIaddCoin();
            winnerCard = p2Card;
            aiCode.Points++;
        }
        else if (battle == 1)
        {
            Debug.Log("P1 WON BATTLE");
            AM.Victory();
            Destroy(p2Card);
            meCode.addCoin();
            winnerCard = p1Card;
            meCode.Points++;
        }
        else
        {
            Debug.Log("TIE");
            AM.tie();
            winnerCard = null;
        }
        if (checkForWin(meCode.Points, aiCode.Points)) ; //somebody wins
        else Invoke("resetStuffs", 1.5F);
    }

    void resetStuffs()
    {
        // reset stuffs & destroy cards
        battle = 0;
        if (winnerCard != null) Destroy(winnerCard);
        else
        {
            Destroy(p1Card);
            Destroy(p2Card);
        }
        p1Card = p2Card =  winnerCard = null;

        // new p2 move
        p2Card = aiCode.whatDoIDo();
        if (eagleEye)   // archer op action
        {
            p2Card.transform.SetParent(dropzone.transform);
            p2Card.GetComponent<Draggable>().enabled = false;
            eagleEye = false;
        }
    }

    bool checkForWin(int p1Score, int p2Score)
    {
            // cant buy more cards
        if(p1.GetComponent<myDeck>().myHand.Count == 0 && p1Score < 1) p2Score = 10;
        if(p2.GetComponent<myDeck>().myHand.Count == 0 && p2Score < 1) p1Score = 10;

            // completely out of cards
        if (p1.GetComponent<myDeck>().myHand.Count == 0 && p1.GetComponent<myDeck>().outOfCards) p2Score = 10;
        if (p2.GetComponent<myDeck>().myHand.Count == 0 && p2.GetComponent<myDeck>().outOfCards) p1Score = 10;

        if (p1Score < 5 && p2Score < 5) return false;
        else
        {
            foreach (GameObject cards in GameObject.FindGameObjectsWithTag("Card")) Destroy(cards);
            p1.GetComponent<myDeck>().cardCount = 0;
            p2.GetComponent<myDeck>().cardCount = 0;

            // disable ability to buy things, dunno other win things

            if (p1Score >= 5)
            {
                // P1 wins, do stuffs
                Debug.Log("P1 Wins!");
                AM.Over();
                endScreen.SetActive(true);
                AM.stahp();
                winner.text = "Player Wins!";
            }
            else
            {
                // P2 wins, od stuffs
                Debug.Log("P2 Wins!");
                AM.Over();
                endScreen.SetActive(true);
                AM.stahp();
                winner.text = "CPU Wins!";
            }

            return true;
        }
    }

    public void P1Turn()
    {
            // wait for cpu to finish
        dropzone.GetComponent<DropZone>().placed = false;
    }
}
