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

    public bool dayOneInteract = false;
    public bool dayTwoInteract = false;
    public bool dayThreeInteract = false;

    public bool hasInteractedOne = false;
    public bool hasInteractedTwo = false;
    public bool hasInteractedThree = false;



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
                        hasInteractedOne = true;
                        canInteract = false;
                        dayOneInteract = true;
                        GameManager.instance.SetToNight(true);
                        
                        MainDialogueManager.instance.StartCampfireDialogue();
                    }
                    else
                    {
                        notReadyPrompt.SetActive(true);
                    }
                }
                else if (DayManager.instance.dayCount == 2)
                {
                    if (GameManager.instance.hasPlayedSolitaire && !dayTwoInteract)
                    {
                        hasInteractedTwo = true;
                        dayTwoInteract = true;
                        canInteract = false;
                        GameManager.instance.LoadRhythm();
                    }
                    else
                    {
                        notReadyPrompt.SetActive(true);
                    }
                }
                else if (DayManager.instance.dayCount == 3)
                {
                    if (LoadYarnVariables.instance.GetBool("hasFished") && LoadYarnVariables.instance.GetBool("hasCooked") && !dayThreeInteract)
                    {
                        hasInteractedThree = true;
                        dayThreeInteract = true;
                        canInteract = false;

                        GameManager.instance.LoadRhythm();

                        // go see alex MainDialogueManager.instance.
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
