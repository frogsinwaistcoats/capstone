using System.Collections;
using UnityEngine;
using TMPro;

public class TentInteractable : MonoBehaviour
{
    private bool playerFound = false;
    [SerializeField] private GameObject prompt;
    public GameObject dayNightAnim;
    public GameObject notReadyPrompt;

    CampManager gameManager;
    DayManager dayManager;

    private void Start()
    {
        gameManager = FindAnyObjectByType<CampManager>();
        dayManager = FindAnyObjectByType<DayManager>();
    }


    private void Update()
    {
        if (playerFound && Input.GetKeyDown(KeyCode.E))
        {
            prompt.SetActive(false);

            if (DayManager.instance.dayCount == 1)
            {
                if (gameObject.CompareTag("Tent") && !GameManager.instance.hasUnpacked)
                {
                    FindAnyObjectByType<AudioManager>().Play("TentZip");
                    GameManager.instance.LoadUnpacking();

                }
                else if (gameObject.CompareTag("Tent") && GameManager.instance.hasUnpacked && GameManager.instance.isDaytime)
                {
                    notReadyPrompt.SetActive(true);
                    notReadyPrompt.GetComponent<TextMeshPro>().text = "Its too early to sleep";

                    //dayManager.StartNewDay();
                    //StartCoroutine(DayNightCutscene(dayNightAnim));
                }
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!GameManager.instance.hasUnpacked)
        {
            playerFound = true;
            prompt.SetActive(true);
            prompt.GetComponent<TextMeshPro>().text = "Unpack? (E)";
        }
        else if (GameManager.instance.hasUnpacked)
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
