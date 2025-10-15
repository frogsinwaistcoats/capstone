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

    public GameObject dayNightPrefab;

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
                    if (GameManager.instance.hasUnpacked && GameManager.instance.metEveryone && !GameManager.instance.campfireStoryRead)
                    {
                        MainDialogueManager.instance.StartCampfireDialogue();
                        canInteract = false;

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

    IEnumerator DayNightCutscene(GameObject obj)
    {
        obj.SetActive(true);
        dayManager.dayCounterText.gameObject.SetActive(false);
        
        yield return new WaitForSeconds(1.5f);
        
        obj.SetActive(false);
        dayManager.dayCounterText.gameObject.SetActive(true);
        
        
    }
}
