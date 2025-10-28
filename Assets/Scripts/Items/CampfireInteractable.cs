using System.Collections;
using TMPro;
using UnityEngine;
using static QuestsList;

public class CampfireInteractable : MonoBehaviour
{
    public static CampfireInteractable instance;

    private bool playerFound = false;
    [SerializeField] private GameObject prompt;
    [SerializeField] private GameObject notReadyPrompt;

    public bool canInteract = true;

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

                if (DayManager.instance.dayCount == 1 && GameManager.instance.isDaytime)
                {
                    if ((LoadYarnVariables.instance.GetBool("$hasUnpacked")) && (LoadYarnVariables.instance.GetInt("$peopleMet") >= 9) && (!LoadYarnVariables.instance.GetBool("$campfireStoryRead")) && GameManager.instance.isDaytime)
                    {
                        canInteract = false;
                        GameManager.instance.SetToNight(true);
                        
                        MainDialogueManager.instance.StartCampfireDialogue();
                        AudioManager.instance.Play("Campfire");
                    }
                    else
                    {
                        notReadyPrompt.SetActive(true);
                    }
                }
                else if (DayManager.instance.dayCount == 2)
                {
                    if (GameManager.instance.hasPlayedSolitaire && GameManager.instance.isDaytime)
                    {
                        LoadYarnVariables.instance.SetYarnVariable("$campfireDay2", true);
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
                    if (LoadYarnVariables.instance.GetBool("$hasFished") && LoadYarnVariables.instance.GetBool("$hasCooked") && GameManager.instance.isDaytime)
                    {
                        LoadYarnVariables.instance.SetYarnVariable("$campfireDay3", true);
                        canInteract = false;
                        GameManager.instance.LoadRhythm();
                    }
                }
                else if (DayManager.instance.dayCount == 4)
                {
                    if (LoadYarnVariables.instance.GetBool("$talkAfterFlowers") && GameManager.instance.isDaytime)
                    {
                        LoadYarnVariables.instance.SetYarnVariable("$campfireDay4", true);
                        canInteract = false;
                        GameManager.instance.LoadRhythm();
                    }
                }


            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
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
