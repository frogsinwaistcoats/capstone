using System.Collections;
using TMPro;
using UnityEngine;

public class CampfireInteractable : MonoBehaviour
{
    public static CampfireInteractable instance;

    private bool playerFound = false;
    [SerializeField] private GameObject prompt;
    [SerializeField] private GameObject notReadyPrompt;

    public bool canInteract = true;

    public NightTransition nightTransition;

    private void Start()
    {
        instance = this;
    }


    private void Update()
    {
        if (!canInteract)
        {
            return;
        }
        else if (canInteract)
        {
            if (playerFound && Input.GetKeyDown(KeyCode.E))
            {
                prompt.SetActive(false);

                if (DayManager.instance.dayCount == 1)
                {
                    if ((LoadYarnVariables.instance.GetBool("$hasUnpacked")) && (LoadYarnVariables.instance.GetInt("$peopleMet") >= 9) && (!LoadYarnVariables.instance.GetBool("$campfireStoryRead")))
                    {
                        canInteract = false;

                        GameManager.instance.SetToNight();

                        nightTransition.gameObject.SetActive(true);
                        nightTransition.PlayTransition();

                        MainDialogueManager.instance.StartCampfireDialogue();
                    }
                    else
                    {
                        notReadyPrompt.SetActive(true);
                    }
                }
                else if (DayManager.instance.dayCount == 2)
                {
                    if (GameManager.instance.hasPlayedSolitaire)
                    {
                        canInteract = false;
                        GameManager.instance.LoadRhythm();
                    }
                    else
                    {
                        notReadyPrompt.SetActive(true);
                    }
                }
            }

        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.instance.isDaytime == false)
        {
            return;
        }
        playerFound = true;
        prompt.SetActive(true);

        prompt.GetComponent<TextMeshPro>().text = "Campfire Song? (E)";
    }

    public void OnTriggerExit(Collider other)
    {
        playerFound = false;
        prompt.SetActive(false);
        notReadyPrompt.SetActive(false);
    }
}
