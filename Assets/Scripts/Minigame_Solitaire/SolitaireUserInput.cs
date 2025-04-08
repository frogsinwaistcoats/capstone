using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SolitaireUserInput : MonoBehaviour
{
    public GameObject slot1;
    private float timer;
    private float doubleClickTime = 0.3f;
    private int clickCount = 0;

    private Solitaire solitaire;

    // Start is called before the first frame update
    void Start()
    {
        solitaire = FindFirstObjectByType<Solitaire>();
        slot1 = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (clickCount == 1)
        {
            timer += Time.deltaTime;
        }
        if (clickCount == 3)
        {
            timer = 0;
            clickCount = 1;
        }
        if (timer > doubleClickTime)
        {
            timer = 0;
            clickCount = 0;
        }

        GetMouseClick();
    }

    void GetMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickCount++;

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10));
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                // what has been hit?
                if (hit.collider.CompareTag("Deck"))
                {
                    //clicked deck
                    Deck();
                }
                else if (hit.collider.CompareTag("Card"))
                {
                    //clicked card
                    Card(hit.collider.gameObject);
                }
                else if (hit.collider.CompareTag("Top"))
                {
                    //clicked top
                    Top(hit.collider.gameObject);
                }
                else if (hit.collider.CompareTag("Bottom"))
                {
                    //clicked bottom
                    Bottom(hit.collider.gameObject);
                }
            }
        }
    }

    void Deck() // deck click actions
    {
        print("clicked on deck");
        //when deck is clicked, cards are dealt
        solitaire.DealFromDeck();
        slot1 = this.gameObject;
    }

    void Card(GameObject selected) // card click actions
    {       
        print("clicked on card");

        if (!selected.GetComponent<SolitaireSelectable>().faceUp) // if the card is face down
        {
            if (!Blocked(selected)) // if the card is not blocked
            {
                // flip it over
                selected.GetComponent<SolitaireSelectable>().faceUp = true;
                slot1 = this.gameObject;
            }
        }
        else if (selected.GetComponent<SolitaireSelectable>().inDeckPile) // if the card is in the deck pile with the trips
        {
            if (!Blocked(selected)) // if the card is not blocked
            {
                if (slot1 == selected) //if the same card is clicked twice
                {
                    if (DoubleClick())
                    {
                        // attempt auto stack
                        AutoStack(selected);
                    }
                }
                else
                {
                    slot1 = selected;
                }
            }
        }
        else
        {
            // if the card is face up
            // if the is no card currently selected
            // select it

            if (slot1 == this.gameObject) //so it will not be null
            {
                slot1 = selected;
            }

            // if there is already a card selected (and it is not the same card)
            else if (slot1 != selected)
            {
                // if the new card is eligible to stack on previous card
                if (Stackable(selected))
                {
                    Stack(selected);
                }
                else
                {
                    // select it
                    slot1 = selected;
                }
            }

            else if (slot1 == selected) //if the same card is clicked twice
            {
                if (DoubleClick())
                {
                    //attempt autostack
                    AutoStack(selected);
                }
            }
            // else if there is already a card selected and it is the same card
            // if the time is short enough then it is a double click
            // if the card is eligible to fly up top then do it
        }
    }

    void Top(GameObject selected) // top click actions
    {
        print("clicked on top");

        if (slot1.CompareTag("Card"))
        {
            //if the card is an ace and the empty slot is top then stack
            if (slot1.GetComponent<SolitaireSelectable>().value == 1)
            {
                Stack(selected);
            }
        }
    }

    void Bottom(GameObject selected) // bottom click actions
    {
        print("clicked on bottom");

        //if the card is a king and the empty slot is bottom then stack
        if (slot1.CompareTag("Card"))
        {
            if (slot1.GetComponent<SolitaireSelectable>().value == 13)
            {
                Stack(selected);
            }
        }
    }

    // if cards can stack or not
    bool Stackable(GameObject selected)
    {
        SolitaireSelectable s1 = slot1.GetComponent<SolitaireSelectable>(); //previous card
        SolitaireSelectable s2 = selected.GetComponent<SolitaireSelectable>(); //current card
                                                             //compare them to see if they stack


        if (!s2.inDeckPile) //stops stacking in deck pile
        {
            if (s2.top) //if in the top pile, they must stack suited Ace to King
            {
                if (s1.suit == s2.suit || (s1.value == 1 && s2.suit == null))
                {
                    if (s1.value == s2.value + 1)
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            //if in the bottom pile, they must stack alternate colours King to Ace
            else
            {
                if (s1.value == s2.value - 1)
                {
                    //checking if cards are same colour
                    bool card1Red = true;
                    bool card2Red = true;

                    if (s1.suit == "C" || s1.suit == "S")
                    {
                        card1Red = false;
                    }
                    if (s2.suit == "C" || s2.suit == "S")
                    {
                        card2Red = false;
                    }

                    if (card1Red == card2Red) //either both red or both black
                    {
                        print("Not Stackable");
                        return false;
                    }
                    else //cards are different colours
                    {
                        print("Stackable");
                        return true;
                    }
                }
            }
        }
        return false;
    }

    void Stack(GameObject selected)
    {
        // if on top of king or empty bottom, stack the cards in place
        //else stack the cards with a negative y offset

        SolitaireSelectable s1 = slot1.GetComponent<SolitaireSelectable>(); //previous card
        SolitaireSelectable s2 = selected.GetComponent<SolitaireSelectable>(); //current card
        float yOffset = 0.5f;

        if (s2.top || (!s2.top && s1.value == 13))
        {
            yOffset = 0;
        }

        slot1.transform.position = new Vector3(selected.transform.position.x, selected.transform.position.y - yOffset, selected.transform.position.z - 0.01f);
        slot1.transform.parent = selected.transform; //this makes the children move with the parents

        if (s1.inDeckPile) //removes the cards from the top pile to prevent duplicates
        {
            solitaire.tripsOnDisplay.Remove(slot1.name);
        }
        else if (s1.top && s2.top && s1.value == 1) //allows movement of cards between top spots
        {
            solitaire.topPos[s1.row].GetComponent<SolitaireSelectable>().value = 0;
            solitaire.topPos[s1.row].GetComponent<SolitaireSelectable>().suit = null; //stops keeping track of that row once ace is on the top
        }
        else if (s1.top) //keeps track of the current value of the top decks as a card has been removed
        {
            solitaire.topPos[s1.row].GetComponent<SolitaireSelectable>().value = s1.value - 1;
        }
        else //removes the card string from the appropriate bottom list
        {
            solitaire.bottoms[s1.row].Remove(slot1.name);
        }

        s1.inDeckPile = false; //you cannot add cards to the trips pile so this is always fine
        s1.row = s2.row;

        if (s2.top) //moves a card to the top and assigns the tops value and suit
        {
            solitaire.topPos[s1.row].GetComponent<SolitaireSelectable>().value = s1.value;
            solitaire.topPos[s1.row].GetComponent<SolitaireSelectable>().suit = s1.suit;
            s1.top = true;
        }
        else
        {
            s1.top = false;
        }

        //after completing move, reset slot1 to be essentially null (null will break the logic)
        slot1 = this.gameObject;
    }

    bool Blocked(GameObject selected)
    {
        SolitaireSelectable s2 = selected.GetComponent<SolitaireSelectable>();
        if (s2.inDeckPile == true)
        {
            if(s2.name == solitaire.tripsOnDisplay.Last()) //if it is the last trip it is not blocked
            {
                return false;
            }
            else
            {
                print(s2.name + " is blocked by " + solitaire.tripsOnDisplay.Last());

                return true;
            }
        }
        else
        {
            if (s2.name == solitaire.bottoms[s2.row].Last()) //check if it is the bottom card
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    bool DoubleClick()
    {
        if (timer < doubleClickTime && clickCount == 2)
        {
            print("Double Click");
            return true;
        }
        else
        {
            return false;
        }
    }

    void AutoStack(GameObject selected)
    {
        for (int i = 0; i < solitaire.topPos.Length; i++)
        {
            SolitaireSelectable stack = solitaire.topPos[i].GetComponent<SolitaireSelectable>();
            if (selected.GetComponent<SolitaireSelectable>().value == 1) // if card is an ace
            {
                if (solitaire.topPos[i].GetComponent<SolitaireSelectable>().value == 0) // and the top position is empty
                {
                    slot1 = selected;
                    Stack(stack.gameObject); // stack the ace up top
                    break;                   // in the first empty position found
                }
            }
            else
            {
                //if it is the same suit and next value
                if ((solitaire.topPos[i].GetComponent<SolitaireSelectable>().suit == slot1.GetComponent<SolitaireSelectable>().suit) && (solitaire.topPos[i].GetComponent<SolitaireSelectable>().value == slot1.GetComponent<SolitaireSelectable>().value - 1))
                {
                    if (HasNoChildren(slot1)) // if it is the last card (if it has no children)
                    {
                        slot1 = selected;
                        // find a top spot that matches the conditions for autostacking if it exists
                        string lastCardName = stack.suit + stack.value.ToString();
                        if (stack.value == 1)
                        {
                            lastCardName = stack.suit + "A";
                        }
                        if (stack.value == 11)
                        {
                            lastCardName = stack.suit + "J";
                        }
                        if (stack.value == 12)
                        {
                            lastCardName = stack.suit + "Q";
                        }
                        if (stack.value == 13)
                        {
                            lastCardName = stack.suit + "K";
                        }
                        GameObject lastCard = GameObject.Find(lastCardName);
                        Stack(lastCard);
                        break;
                    }
                }
            }
        }
    }

    bool HasNoChildren(GameObject card)
    {
        int i = 0;
        foreach (Transform child in card.transform)
        {
            i++;
        }
        if (i == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
