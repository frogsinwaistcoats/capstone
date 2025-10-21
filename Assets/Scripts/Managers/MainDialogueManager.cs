using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn;
using Yarn.Unity;

public class MainDialogueManager : MonoBehaviour
{
    public static MainDialogueManager instance;

    public PlayerMovement playerMovement;
    DayManager dayManager;

    private InMemoryVariableStorage variableStorage;

    private bool isCurrentConversation = false;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        FindAnyObjectByType<DialogueRunner>().onDialogueComplete.AddListener(EndConversation);
        dayManager = FindAnyObjectByType<DayManager>();

        if (dayManager.dayCount == 1 && !LoadYarnVariables.instance.GetBool("$hasDoneIntro"))
            StartConversation("MrWilson_Intro");
    }

    private void Update()
    {
        /*
        variableStorage = GameObject.FindAnyObjectByType<InMemoryVariableStorage>();
        bool playRhythm;
        variableStorage.TryGetValue("$playRhythm", out playRhythm);
        if (playRhythm)
        {
            GameManager.instance.LoadRhythm();
        }
        */
    }

    private void StartConversation(string dialogueNode)
    {
        FindAnyObjectByType<PlayerMovement>().SetMovement(false);
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

            CampfireInteractable.instance.canInteract = true;
            TentInteractable.instance.canInteract = true;
        }
    }

    public void StartCampfireDialogue()
    {
        GameManager.instance.SetToNight();
        StartConversation("Campfire_Story");
    }

    public void ErnestThoughts()
    {
        StartConversation("ErnestThoughts");
    }

    public void StartDayTwoDialogue()
    {
        StartConversation("Ruby_Day2");
    }

    public void SneakingOutDialogue()
    {
        StartConversation("Ernest_SneakingOut");
    }
}


