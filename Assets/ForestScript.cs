using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class ForestScript : MonoBehaviour
{
    public static ForestScript current;

    [SerializeField] private GameObject dialogueVisuals;
    //[SerializeField] private TextMeshProUGUI dialogueName;
    [SerializeField] private TextMeshProUGUI dialogueTextbox;
    [SerializeField] private Image characterImage;
    [SerializeField] private Sprite sprite;

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
        if (playerFound)
        {
            StartCoroutine(DialogueDelay());
            playerMovement.SetMovement(false);
            dialogueVisuals.SetActive(true);
            //dialogueName.text = characterName;
            characterImage.sprite = sprite;
            dialogueTextbox.text = dialogue;
            isInteracting = true;
        }

        if (isInteracting)
        {
            if (dialogueFinished && Input.GetKeyDown(KeyCode.E))
            {
                EndDialogue();
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        playerFound = true;
    }

    public void OnTriggerExit(Collider other)
    {
        EndDialogue();
    }

    public void EndDialogue()
    {
        playerFound = false;
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
}