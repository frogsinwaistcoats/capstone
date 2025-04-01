using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class DialogueTest : MonoBehaviour
{
    /*
    public static DialogueTest current;

    [SerializeField] private GameObject prompt;
    [SerializeField] private GameObject dialogueVisuals;
    //[SerializeField] private TextMeshProUGUI dialogueName;
    [SerializeField] private TextMeshProUGUI dialogueTextbox;
    [SerializeField] private Image characterImage;

    //[TextArea] public string characterName;
    [TextArea] public string dialogue;

    PlayerMovement playerMovement;

    bool playerFound;
    bool isInteracting;
    bool dialogueFinished;

    void Start()
    {
        current = this;
        dialogueVisuals.SetActive(false);
        isInteracting = false;
        dialogueFinished = false;
        playerMovement = FindFirstObjectByType<PlayerMovement>();
    }

    private void Update()
    {
        if (playerFound && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(DialogueDelay());
            playerMovement.SetMovement(false);
            dialogueVisuals.SetActive(true);
            prompt.SetActive(false);
            //dialogueName.text = characterName;
            dialogueTextbox.text = dialogue;
            characterImage.sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
            isInteracting = true;
        }

        if (isInteracting)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("MinigameSolitaire");
            }
            if (dialogueFinished && Input.GetKeyDown(KeyCode.E))
            {
                EndDialogue();
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
        EndDialogue();
    }

    public void EndDialogue()
    {
        playerFound = false;
        prompt.SetActive(false);
        dialogueVisuals.SetActive(false);
        isInteracting = false;
        playerMovement.SetMovement(true);
        dialogueFinished = false;
    }

    public IEnumerator DialogueDelay()
    {
        yield return new WaitForSeconds(1f);
        dialogueFinished = true;
    }
    */
}
