using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolitaireUpdateSprite : MonoBehaviour
{
    public Sprite cardFace;
    public Sprite cardBack;

    private SpriteRenderer spriteRenderer;
    private SolitaireSelectable selectable;
    private Solitaire solitaire;
    private SolitaireUserInput userInput;

    // Start is called before the first frame update
    void Start()
    {
        //generates matching deck
        List<string> deck = Solitaire.GenerateDeck();
#pragma warning disable 0618
        solitaire = FindObjectOfType<Solitaire>();
        userInput = FindObjectOfType<SolitaireUserInput>();
#pragma warning restore 0618

        //matches card face to card name
        int i = 0;
        foreach (string card in deck)
        {
            if (this.name == card)
            {
                cardFace = solitaire.cardFaces[i];
                break;
            }
            i++;
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
        selectable = GetComponent<SolitaireSelectable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (selectable.faceUp == true)
        {
            spriteRenderer.sprite = cardFace;
        }
        else
        {
            spriteRenderer.sprite = cardBack;
        }
        
        if (userInput.slot1)
        {
            // move this to only when card is clicked
            if (name == userInput.slot1.name)
            {
                // highlights selected card
                spriteRenderer.color = Color.yellow;
            }
            else
            {
                spriteRenderer.color = Color.white;
            }
        }
    }
}
