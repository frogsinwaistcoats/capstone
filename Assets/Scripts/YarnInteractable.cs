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

    bool playerFound;

    void Start()
    {
        dialogueRunner = FindAnyObjectByType<DialogueRunner>();
        dialogueRunner.onDialogueComplete.AddListener(EndConversation);
        playerMovement = FindAnyObjectByType<PlayerMovement>();
    }

    public void OnMouseDown()
    {
        
    }

    private void Update()
    {
        if (playerFound && Input.GetKeyDown(KeyCode.E))
        {
            prompt.SetActive(false);
            playerMovement.SetMovement(false);
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
            SceneManager.LoadScene("MinigameSolitaire");
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
            Debug.Log($"Started conversation with {name}.");
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
    }
}
