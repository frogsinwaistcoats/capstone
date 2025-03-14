using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Solitaire : MonoBehaviour
{
    // attached to solitaire game object (like game manager)

    public Sprite[] cardFaces;
    public GameObject cardPrefab;
    public GameObject deckButton;
    public GameObject[] bottomPos;
    public GameObject[] topPos;

    public static string[] suits = new string[] { "C", "D", "H", "S" };
    public static string[] values = new string[] { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
    public List<string>[] bottoms;
    public List<string>[] tops;
    public List<string> tripsOnDisplay = new List<string>(); //current triple on display
    public List<List<string>> deckTrips = new List<List<string>>(); //list of all triples in deck

    //lists of cards in each stack on table
    private List<string> bottom0 = new List<string>();
    private List<string> bottom1 = new List<string>();
    private List<string> bottom2 = new List<string>();
    private List<string> bottom3 = new List<string>();
    private List<string> bottom4 = new List<string>();
    private List<string> bottom5 = new List<string>();
    private List<string> bottom6 = new List<string>();

    public List<string> deck; //list of all cards
    public List<string> discardPile = new List<string>();
    private int deckLocation;
    private int trips; //groups of three cards
    private int tripsRemainder; //leftover cards

    // Start is called before the first frame update
    void Start()
    {
        //list of all cards in bottoms
        bottoms = new List<string>[] { bottom0, bottom1, bottom2, bottom3, bottom4, bottom5, bottom6 };
        PlayCards();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayCards()
    {
        foreach (List<string> list in bottoms)
        {
            list.Clear();
        }

        deck = GenerateDeck();
        Shuffle(deck);

        //test the cards in the deck
        foreach (string card in deck)
        {
            print(card);
        }
        SolitaireSort();// sort cards into bottoms and deck
        StartCoroutine(SolitaireDeal()); //deal cards
        SortDeckIntoTrips();
    }

    //makes list of all cards (adds all cards to the deck)
    public static List<string> GenerateDeck()
    {
        List<string> newDeck = new List<string>();
        foreach (string s in suits)
        {
            foreach (string v in values)
            {
                newDeck.Add(s + v);
            }
        }

        return newDeck;
    }

    //shuffles cards in deck (based on fisher yates shuffle)
    void Shuffle<T>(List<T> list)
    {
        System.Random random = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            int k = random.Next(n);
            n--;
            T temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }
    }

    // deals cards onto screen
    IEnumerator SolitaireDeal()
    {
        // deals cards in bottoms list to correct locations
        for (int i = 0; i < 7; i++)
        {
            float yOffset = 0;
            float zOffset = 0.03f; //to help with rendering and hit detection
            foreach (string card in bottoms[i])
            {
                yield return new WaitForSeconds(0.05f); //delay so cards deal slower
                GameObject newCard = Instantiate(cardPrefab, new Vector3(bottomPos[i].transform.position.x, bottomPos[i].transform.position.y - yOffset, bottomPos[i].transform.position.z - zOffset), Quaternion.identity, bottomPos[i].transform); //added as children of location
                newCard.name = card; //prefab named to match card
                newCard.GetComponent<SolitaireSelectable>().row = i; //sets row of card
                
                //cards only face up if they are the top card
                if (card == bottoms[i][bottoms[i].Count - 1])
                {
                    newCard.GetComponent<SolitaireSelectable>().faceUp = true;
                }

                yOffset = yOffset + 0.35f;
                zOffset = zOffset + 0.03f;
                discardPile.Add(card);
            }
        }

        //clears discard pile
        foreach (string card in discardPile)
        {
            if (deck.Contains(card))
            {
                deck.Remove(card);
            }
        }
        discardPile.Clear();
    }

    // adds cards to bottoms list
    void SolitaireSort()
    {
        for (int i = 0; i < 7; i++)
        {
            for (int j = i; j < 7; j++)
            {
                bottoms[j].Add(deck.Last<string>());
                deck.RemoveAt(deck.Count - 1);
            }
        }

        //when i = 0, 7 cards are added to the bottoms list, one at each location on the table
        //second time through, j starts at 1, so location 0 is skipped
    }

    // sort deck into groups of three
    public void SortDeckIntoTrips()
    {
        trips = deck.Count / 3;
        tripsRemainder = deck.Count % 3;
        deckTrips.Clear(); //reset trips so no duplicate cards

        //creating lists of triples (which are lists of three cards)
        int modifier = 0;
        for (int i = 0; i < trips; i++)
        {
            List<string> myTrips = new List<string>();
            for (int j = 0; j < 3; j++)
            {
                myTrips.Add(deck[j + modifier]);
            }
            deckTrips.Add(myTrips);
            modifier = modifier + 3;
        }
        //if cards are not divisible by three
        if (tripsRemainder != 0)
        {
            List<string> myRemainders = new List<string>();
            modifier = 0;
            for (int k = 0; k < tripsRemainder; k++)
            {
                myRemainders.Add(deck[deck.Count - tripsRemainder + modifier]);
                modifier++;
            }
            deckTrips.Add(myRemainders);
            trips++;
        }
        deckLocation = 0;
    }

    //deals triples from deck
    public void DealFromDeck()
    {
        // add remaining cards to discard pile
        foreach (Transform child in deckButton.transform)
        {
            if (child.CompareTag("Card"))
            {
                deck.Remove(child.name);
                discardPile.Add(child.name);
                Destroy(child.gameObject);
            }
        }

        if (deckLocation < trips)
        {
            // draw 3 new cards
            tripsOnDisplay.Clear();
            float yOffset = -2.5f;
            float zOffset = -0.2f;

            foreach (string card in deckTrips[deckLocation])
            {
                GameObject newTopCard = Instantiate(cardPrefab, new Vector3(deckButton.transform.position.x, deckButton.transform.position.y + yOffset, deckButton.transform.position.z + zOffset), Quaternion.identity, deckButton.transform);
                yOffset = yOffset - 0.5f;
                zOffset = zOffset - 0.2f;
                newTopCard.name = card;
                tripsOnDisplay.Add(card);
                newTopCard.GetComponent<SolitaireSelectable>().faceUp = true;
                newTopCard.GetComponent<SolitaireSelectable>().inDeckPile = true;
            }
            deckLocation++;
        }
        else
        {
            RestackTopDeck();
        }
    }

    //when all cards have been drawn, deck is reset
    void RestackTopDeck()
    {
        deck.Clear();
        foreach (string card in discardPile)
        {
            deck.Add(card);
        }
        discardPile.Clear();
        SortDeckIntoTrips();
    }

}
