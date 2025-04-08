using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SolitaireUIButtons : MonoBehaviour
{
    
    public GameObject highScorePanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAgain()
    {
        highScorePanel.SetActive(false);
        ResetScene();
    }

    
    public void ResetScene()
    {

        // find all the cards and remove them
        SolitaireUpdateSprite[] cards = FindObjectsByType<SolitaireUpdateSprite>(FindObjectsSortMode.None);
        foreach (SolitaireUpdateSprite card in cards)
        {
            Destroy(card.gameObject);
        }

        ClearTopValues();
        // deal new cards
        Solitaire solitaire = FindFirstObjectByType<Solitaire>();
        solitaire.PlayCards();
    }

    public void ClearTopValues()
    {
        SolitaireSelectable[] selectables = FindObjectsByType<SolitaireSelectable>(FindObjectsSortMode.None);
        foreach (SolitaireSelectable selectable in selectables)
        {
            if (selectable.CompareTag("Top"))
            {
                selectable.suit = null;
                selectable.value = 0;
            }
        }
    }

    public void QuitMinigame()
    {
        SceneManager.LoadScene(1);
    }
}
