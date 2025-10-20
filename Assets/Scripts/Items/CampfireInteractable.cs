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

    CampManager gameManager;
    DayManager dayManager;

    public NightTransition nightTransition;

    private void Start()
    {
        instance = this;

        gameManager = FindAnyObjectByType<CampManager>();
        dayManager = FindAnyObjectByType<DayManager>();
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

                if (dayManager.dayCount == 1)
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
                else if (dayManager.dayCount == 2)
                {
                    //add day 2 variables here

                }
            }

        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        playerFound = true;
        prompt.SetActive(true);
        prompt.GetComponent<TextMeshPro>().text = "Campfire story? (E)";

    }

    public void OnTriggerExit(Collider other)
    {
        playerFound = false;
        prompt.SetActive(false);
        notReadyPrompt.SetActive(false);
    }
}
