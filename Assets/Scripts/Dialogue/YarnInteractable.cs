using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;
using Yarn;

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

    private GameObject otherUI;

    void Start()
    {
        otherUI = GameObject.Find("---- UI ----");

        dialogueRunner = FindAnyObjectByType<DialogueRunner>();
        dialogueRunner.onDialogueComplete.AddListener(EndConversation);
        playerMovement = FindAnyObjectByType<PlayerMovement>();
        gameManager = FindAnyObjectByType<GameManager>();

        startPos = transform.position;
    }

    public void OnMouseDown()
    {
        
    }

    private void Update()
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
    }

    private void EndConversation()
    {
        if (isCurrentConversation)
        {
            isCurrentConversation = false;
            Debug.Log($"Ended conversation with {name}.");
            playerMovement.SetMovement(true);
        }
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
