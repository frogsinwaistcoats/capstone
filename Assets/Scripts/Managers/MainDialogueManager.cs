using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn;
using Yarn.Unity;

public class MainDialogueManager : MonoBehaviour
{
    public static MainDialogueManager instance;

    public PlayerMovement playerMovement;

    private InMemoryVariableStorage variableStorage;

    private bool isCurrentConversation = false;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        FindAnyObjectByType<DialogueRunner>().onDialogueComplete.AddListener(EndConversation);

        if (DayManager.instance.dayCount == 1 && !LoadYarnVariables.instance.GetBool("$hasDoneIntro"))
            StartConversation("MrWilson_Intro");
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
            FindAnyObjectByType<PlayerMovement>().SetMovement(true);

            CampfireInteractable.instance.canInteract = true;
            TentInteractable.instance.canInteract = true;
        }
    }

    public void StartCampfireDialogue()
    {
        //GameManager.instance.SetToNight(true);
        StartConversation("Campfire_Story");
    }

    public void ErnestDayOne()
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

    public void FirstMeetingDialogue()
    {
        StartConversation("FirstMeeting");
    }

    public void StartDayThreeDialogue()
    {
        StartConversation("MrWilson_Day3Prep");
    }

    public void DayThreeSneakingPrompt()
    {
        StartConversation("Ernest_Day3Sneaking");
    }

    public void SecondMeetingDialogue()
    {
        StartConversation("SecondMeeting");
    }

    public void AfterLeafDialogue()
    {
        StartConversation("AfterLeaf");
    }

    public void TeacherConfrontation()
    {
        StartConversation("TeacherConfrontation");
    }
}


