using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class MainDialogueManager : MonoBehaviour
{
    public static MainDialogueManager instance;

    public PlayerMovement playerMovement;
    DayManager dayManager;

    private bool isCurrentConversation = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        FindAnyObjectByType<DialogueRunner>().onDialogueComplete.AddListener(EndConversation);
        dayManager = FindAnyObjectByType<DayManager>();

        if (dayManager.dayCount == 1)
            StartConversation("MrWilson_Intro");
    }

    private void StartConversation(string dialogueNode)
    {
        playerMovement.SetMovement(false);
        Debug.Log($"Started conversation with {name}.");
        isCurrentConversation = true;
        FindAnyObjectByType<DialogueRunner>().StartDialogue(dialogueNode);
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
}
