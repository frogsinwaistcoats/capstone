using System.Collections;
using UnityEngine;
using TMPro;

public class TentInteractable : MonoBehaviour
{
    public static TentInteractable instance;

    private bool playerFound = false;
    [SerializeField] private GameObject prompt;
    public GameObject notReadyPrompt;

    public DayTransition dayTransition;
    public bool canInteract = true;

    private void Start()
    {
        instance = this;
    }


    private void Update()
    {
        if (playerFound && Input.GetKeyDown(KeyCode.E))
        {
            if (!canInteract)
            {
                return;
            }
            else if (canInteract)
            {
                prompt.SetActive(false);

                if (DayManager.instance.dayCount == 1)
                {
                    if (gameObject.CompareTag("Tent") && !LoadYarnVariables.instance.GetBool("$hasUnpacked"))
                    {
                        FindAnyObjectByType<AudioManager>().Play("TentZip");
                        GameManager.instance.LoadUnpacking();

                    }
                    else if (gameObject.CompareTag("Tent") && !GameManager.instance.CanSleep())
                    {
                        notReadyPrompt.SetActive(true);
                        notReadyPrompt.GetComponent<TextMeshPro>().text = "Its too early to sleep";
                    }
                    else if (gameObject.CompareTag("Tent") && GameManager.instance.CanSleep())
                    {
                        canInteract = false;
                        prompt.SetActive(false);

                        FindAnyObjectByType<AudioManager>().Play("TentZip");

                        dayTransition.gameObject.SetActive(true);
                        dayTransition.PlayTransition();

                        DayManager.instance.StartNewDay(2);
                        GameManager.instance.SetToDay();
                        MainDialogueManager.instance.StartDayTwoDialogue();
                    }
                }
                else if (DayManager.instance.dayCount == 2)
                {
                    if (gameObject.CompareTag("Tent") && !GameManager.instance.CanSleep())
                    {
                        notReadyPrompt.SetActive(true);
                        notReadyPrompt.GetComponent<TextMeshPro>().text = "Its too early to sleep";
                    }
                    else if (gameObject.CompareTag("Tent") && GameManager.instance.CanSleep())
                    {
                        canInteract = false;
                        prompt.SetActive(false);

                        FindAnyObjectByType<AudioManager>().Play("TentZip");

                        dayTransition.gameObject.SetActive(true);
                        dayTransition.PlayTransition();

                        DayManager.instance.StartNewDay(3);
                        GameManager.instance.SetToDay();
                        MainDialogueManager.instance.StartDayThreeDialogue();
                    }
                }
                else if (DayManager.instance.dayCount == 3)
                {
                    if (gameObject.CompareTag("Tent") && !GameManager.instance.CanSleep())
                    {
                        notReadyPrompt.SetActive(true);
                        notReadyPrompt.GetComponent<TextMeshPro>().text = "Its too early to sleep";
                    }
                    else if (gameObject.CompareTag("Tent") && GameManager.instance.CanSleep())
                    {
                        canInteract = false;
                        prompt.SetActive(false);

                        FindAnyObjectByType<AudioManager>().Play("TentZip");

                        dayTransition.gameObject.SetActive(true);
                        dayTransition.PlayTransition();

                        DayManager.instance.StartNewDay(4);
                        GameManager.instance.SetToDay();
                        //MainDialogueManager.instance.StartDayThreeDialogue();
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (DayManager.instance.dayCount == 1)
        {
            if (!LoadYarnVariables.instance.GetBool("$hasUnpacked"))
            {
                playerFound = true;
                prompt.SetActive(true);
                prompt.GetComponent<TextMeshPro>().text = "Unpack? (E)";
            }
            else if (LoadYarnVariables.instance.GetBool("$hasUnpacked"))
            {
                playerFound = true;
                prompt.SetActive(true);
                prompt.GetComponent<TextMeshPro>().text = "Sleep? (E)";
            }
        }
        else
        {
            playerFound = true;
            prompt.SetActive(true);
            prompt.GetComponent<TextMeshPro>().text = "Sleep? (E)";
        }

    }

    public void OnTriggerExit(Collider other)
    {
        playerFound = false;
        prompt.SetActive(false);
        notReadyPrompt.SetActive(false);
    }

}
