using System.Collections;
using UnityEngine;
using TMPro;

public class TentInteractable : MonoBehaviour
{
    private bool playerFound = false;
    [SerializeField] private GameObject prompt;
    public GameObject cutscenePrefab;

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
            FindAnyObjectByType<AudioManager>().Play("TentZip");
            prompt.SetActive(false);

            if (gameObject.CompareTag("Tent") && !GameManager.instance.hasUnpacked)
            {
                GameManager.instance.LoadUnpacking();

            }
            else if (gameObject.CompareTag("Tent") && GameManager.instance.hasUnpacked)
            {
                //dayManager.StartNewDay();
                //StartCoroutine(DayNightCutscene(cutscenePrefab));
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
            //playerFound = true;
            //prompt.SetActive(true);
            //prompt.GetComponent<TextMeshPro>().text = "Sleep? (E)";
        }

    }

    public void OnTriggerExit(Collider other)
    {
        playerFound = false;
        prompt.SetActive(false);
    }

    IEnumerator DayNightCutscene(GameObject obj)
    {
        obj.SetActive(true);
        dayManager.dayCounterText.gameObject.SetActive(false);
        yield return new WaitForSeconds(2);
        obj.SetActive(false);
        dayManager.dayCounterText.gameObject.SetActive(true);
    }

}
