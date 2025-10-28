using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;
using Yarn;
using System.Collections;

public class YarnInteractable : MonoBehaviour
{
    PlayerMovement playerMovement;

    [SerializeField] private string conversationStartNode;
    [SerializeField] private GameObject prompt;

    private DialogueRunner dialogueRunner;

    private bool interactable = true;
    private bool isCurrentConversation = false;
    private InMemoryVariableStorage variableStorage;

    private GameManager gameManager;

    bool playerFound;

    Vector3 startPos;

    bool canInteract = true;


    void Start()
    {
        dialogueRunner = FindAnyObjectByType<DialogueRunner>();
        dialogueRunner.onDialogueComplete.AddListener(EndConversation);
        playerMovement = FindAnyObjectByType<PlayerMovement>();
        gameManager = FindAnyObjectByType<GameManager>();

        startPos = transform.position;
    }

    private void Update()
    {
        if (canInteract && MainDialogueManager.instance.canInteract)
        {
            if (playerFound && Input.GetKeyDown(KeyCode.E))
            {
                prompt.SetActive(false);
                playerMovement.canMove = false;
                playerMovement.animator.SetBool("isMoving", false);
                if (interactable && !dialogueRunner.IsDialogueRunning)
                {
                    StartConversation();
                }
            }
        }

        variableStorage = GameObject.FindAnyObjectByType<InMemoryVariableStorage>();
        bool playSolitaire;
        variableStorage.TryGetValue("$playSolitaire", out playSolitaire);
        if (playSolitaire)
        {
            gameManager.LoadSolitaire();
        }

        bool sneakingOut;
        variableStorage.TryGetValue("$sneakingOut", out sneakingOut);
        if (sneakingOut)
        {
            SceneManager.LoadScene("Sneaking");
        }

        bool playCooking;
        variableStorage.TryGetValue("$playCooking", out playCooking);
        if (playCooking)
        {
            SceneManager.LoadScene("Cooking");
        }


    }

    private void StartConversation()
    {
        Debug.Log($"Started conversation with {name}.");
        isCurrentConversation = true;
        dialogueRunner.StartDialogue(conversationStartNode);
        playerMovement.canMove = false;
        interactable = false;
        canInteract = false;
    }

    private void EndConversation()
    {
        if (isCurrentConversation)
        {
            isCurrentConversation = false;
            Debug.Log($"Ended conversation with {name}.");
            playerMovement.SetMovement(true);
            StartCoroutine(WaitToInteract());
            canInteract = true;
        }
    }

    public IEnumerator WaitToInteract()
    {
        yield return new WaitForSeconds(0.2f);
        interactable = true;
        PlayerMovement.instance.canMove = true;
    }

    [YarnCommand("disable")]
    public void DisableConversation()
    {
        interactable = false;
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
    }

    public void MoveWhileNight()
    {
        transform.position = new Vector3(100f, 0f, 0f);
    }

    public void ReturnToStart()
    {
        transform.position = startPos;
    }
}
