using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NPC : MonoBehaviour
{
    public RuntimeAnimatorController dialogueController; //single dialogue
    //multiple dialogues
    public List<DialogueConditions> dialogueConditions = new List<DialogueConditions>();
    public UnityEvent enterEvent;
    public UnityEvent exitEvent;
    public UnityEvent finishedDialogueEvent;
    bool usingCondition;
    int conditionIndex;
    bool playerFound;
    bool isTalking;

    private void Update()
    {
        if (playerFound == true & Input.GetKeyDown(KeyCode.E))
        {
            if(isTalking == true)
            {
                DialogueManager.current.NextDialogue();
                return;
            }
            
            if (dialogueController == null)
            {
                foreach(DialogueConditions checkDialogueCondition in dialogueConditions)
                {
                    bool conditionMet = CheckCondition(checkDialogueCondition);
                    if (conditionMet)
                    {
                        if(checkDialogueCondition.dialogueController != null)
                        {
                            isTalking = true;
                            PlayerMovement.current.SetMovement(false);
                            exitEvent.Invoke();
                            usingCondition = true;
                            conditionIndex = dialogueConditions.IndexOf(checkDialogueCondition);
                            DialogueManager.current.StartDialogue(this, checkDialogueCondition.dialogueController);
                            return;
                        }
                        else
                        {
                            checkDialogueCondition.finishedDialogueEvent.Invoke();
                            return;
                        }
                    }
                }

                exitEvent.Invoke();
                FinishedDialogue();
                return;
            }

            isTalking = true;
            PlayerMovement.current.SetMovement(false);
            exitEvent.Invoke();
            DialogueManager.current.StartDialogue(this, dialogueController);
        }
    }

    bool CheckCondition(DialogueConditions checkDialogueCondition)
    {
        //if adding conditions to dialogue like quests, add here
        switch (checkDialogueCondition.dialogueConditionType)
        {
            case DialogueConditions.DialogueConditionType.Default:
                return true;
        }
        return false;
    }

    public void FinishedDialogue()
    {
        PlayerMovement.current.SetMovement(true);
        isTalking = false;
        finishedDialogueEvent.Invoke();
        if (usingCondition)
        {
            dialogueConditions[conditionIndex].finishedDialogueEvent.Invoke();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        enterEvent.Invoke();
        playerFound = true;
    }

    public void OnTriggerExit(Collider other)
    {
        exitEvent.Invoke();
        playerFound = false;
        isTalking = false;
    }
}

public class DialogueConditions
{
    public DialogueConditionType dialogueConditionType;
    public RuntimeAnimatorController dialogueController;
    public UnityEvent finishedDialogueEvent;

    public enum DialogueConditionType
    {
        Default
    }
}
