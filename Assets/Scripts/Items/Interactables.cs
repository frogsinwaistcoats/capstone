using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interactables : MonoBehaviour
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
            prompt.SetActive(false);

            if (gameObject.CompareTag("Tent"))
            {
                dayManager.StartNewDay();
                StartCoroutine(DayNightCutscene(cutscenePrefab));
               
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        playerFound = true;
        prompt.SetActive(true);
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
        yield return new WaitForSeconds(3);
        obj.SetActive(false);
        dayManager.dayCounterText.gameObject.SetActive(true);
    }

}
