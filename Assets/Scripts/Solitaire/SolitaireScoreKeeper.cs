using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolitaireScoreKeeper : MonoBehaviour
{
    public SolitaireSelectable[] topStacks;
    public GameObject winScreen;


    // Update is called once per frame
    void Update()
    {
        if (HasWon())
        {
            Win();
        }
    }

    public bool HasWon()
    {
        int i = 0;
        foreach (SolitaireSelectable topstack in topStacks)
        {
            i += topstack.value;
        }
        if (i >= 52)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Win()
    {
        winScreen.SetActive(true);
        print("You have won!");
    }
}
