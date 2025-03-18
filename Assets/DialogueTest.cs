using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueTest : MonoBehaviour
{
    public static DialogueTest current;

    [SerializeField] private GameObject prompt;
    [SerializeField] private GameObject dialogueVisuals;

    bool playerFound;
    bool isInteracting;

    void Start()
    {
        current = this;
        dialogueVisuals.SetActive(false);
        isInteracting = false;
    }

    private void Update()
    {
        if (playerFound == true & Input.GetKeyDown(KeyCode.E))
        {
            isInteracting = true;
            dialogueVisuals.SetActive(true);
            prompt.SetActive(false);
        }

        if (isInteracting)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("MinigameSolitaire");
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        playerFound = true;
        prompt.SetActive(true);
    }

    public void OnTriggerExit(Collider other)
    {
        playerFound = false;
        prompt.SetActive(false);
        dialogueVisuals.SetActive(false);
        isInteracting = false;
    }
}
