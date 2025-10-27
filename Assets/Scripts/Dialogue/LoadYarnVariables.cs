using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn;
using Yarn.Unity;

public class LoadYarnVariables : MonoBehaviour
{
    public static LoadYarnVariables instance;
    private DialogueRunner dialogueRunner;

    int day;
    bool isDaytime;
    bool playSolitaire;
    bool playRhythm;
    bool playFishing;
    bool playCooking;
    bool playLeaf;
    bool playSpotlight;

    bool talkedToNyrie;
    bool talkedToTalia;
    bool talkedToRuby;
    bool talkedToPepper;
    bool talkedToPoppy;
    bool talkedToMillie;
    bool talkedToWilson;
    bool talkedToLily;
    bool talkedToAngler;
    int peopleMet;

    bool hasUnpacked;
    bool hasDoneIntro;
    bool campfireStoryRead;

    bool campfireDay2;
    bool caughtByTeacher;
    bool triggerSneakOut;
    bool firstMeetingDone;

    bool campfireDay3;
    bool hasFished;
    bool hasCooked;
    bool hasDoneLeaf;

    bool campfireDay4;
    bool canUseCamera;
    bool hasGotFlowers;
    bool talkAfterFlowers;
    bool hasDoneSpotlight;
    bool goToForestDay4;

    bool endScene;
    bool ernestThinking;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        if (dialogueRunner != null)
        {
            dialogueRunner.onDialogueComplete?.RemoveListener(PullFromYarnVariables);
            dialogueRunner.onNodeComplete?.RemoveListener(OnNodeComplete);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(InitialiseSceneObjects());
    }

    private IEnumerator InitialiseSceneObjects()
    {
        yield return null;
        dialogueRunner = FindFirstObjectByType<DialogueRunner>(); //only exists in CampScene

        if (dialogueRunner != null)
        {
            // re wire events
            dialogueRunner.onDialogueComplete?.RemoveListener(PullFromYarnVariables);
            dialogueRunner.onDialogueComplete?.AddListener(PullFromYarnVariables);

            dialogueRunner.onNodeComplete?.RemoveListener(OnNodeComplete);
            dialogueRunner.onNodeComplete?.AddListener(OnNodeComplete);

            // initialize sync
            UpdateAllYarnVariables();
            PullFromYarnVariables();

        }
    }

    private void OnNodeComplete(string _)
    {
        PullFromYarnVariables();
    }

    // C# to Yarn
    public void UpdateAllYarnVariables()
    {
        if (dialogueRunner == null) return;
        var vs = dialogueRunner.VariableStorage;

        // day count and minigame plays
        vs.SetValue("$day", (float)day);
        vs.SetValue("$isDaytime", isDaytime);
        vs.SetValue("$playSolitaire", playSolitaire);
        vs.SetValue("$playRhythm", playRhythm);
        vs.SetValue("$playFishing", playFishing);
        vs.SetValue("$playCooking", playCooking);
        vs.SetValue("$playLeaf", playLeaf);
        vs.SetValue("$playSpotlight", playSpotlight);

        // people talked to on day 1
        vs.SetValue("$talkedToNyrie", talkedToNyrie);
        vs.SetValue("$talkedToTalia", talkedToTalia);
        vs.SetValue("$talkedToRuby", talkedToRuby);
        vs.SetValue("$talkedToPepper", talkedToPepper);
        vs.SetValue("$talkedToPoppy", talkedToPoppy);
        vs.SetValue("$talkedToMillie", talkedToMillie);
        vs.SetValue("$talkedToWilson", talkedToWilson);
        vs.SetValue("$talkedToLily", talkedToLily);
        vs.SetValue("$talkedToAngler", talkedToAngler);
        vs.SetValue("$peopleMet", (float)peopleMet);

        // day 1 progress
        vs.SetValue("$hasUnpacked", hasUnpacked);
        vs.SetValue("$hasDoneIntro", hasDoneIntro);
        vs.SetValue("$campfireStoryRead", campfireStoryRead);

        // day 2 progress
        vs.SetValue("$caughtByTeacher", caughtByTeacher);
        vs.SetValue("$triggerSneakOut", triggerSneakOut);
        vs.SetValue("$firstMeetingDone", firstMeetingDone);
        vs.SetValue("$campfireDay2", campfireDay2);

        // day 3 progress
        vs.SetValue("$hasFished", hasFished);
        vs.SetValue("$hasCooked", hasCooked);
        vs.SetValue("$campfireDay3", campfireDay3);
        vs.SetValue("$hasDoneLeaf", hasDoneLeaf);

        //day 4 progress
        vs.SetValue("$canUseCamera", canUseCamera);
        vs.SetValue("$hasGotFlowers", canUseCamera);
        vs.SetValue("$campfireDay4", campfireDay4);
        vs.SetValue("$talkAfterFlowers", talkAfterFlowers);
        vs.SetValue("$hasDoneSpotlight", hasDoneSpotlight);
        vs.SetValue("$goToForestDay4", goToForestDay4);

        //day 5 progress
        vs.SetValue("$endScene", endScene);
        vs.SetValue("$ernestThinking", ernestThinking);
    }

    // Yarn to C#
    public void PullFromYarnVariables()
    {
        if (dialogueRunner == null) return;
        var vs = dialogueRunner.VariableStorage;

        vs.TryGetValue("$day", out float dayFloat);
        day = Mathf.RoundToInt(dayFloat);

        vs.TryGetValue("$playSolitaire", out playSolitaire);
        vs.TryGetValue("$playRhythm", out playRhythm);
        if (playRhythm)
        {
            playRhythm = false;
            SetYarnVariable("$playSolitaire", false);

            if (GameManager.instance != null)
            {
                GameManager.instance.LoadRhythm();
            }
        }
        vs.TryGetValue("$playFishing", out playFishing);
        if (playFishing)
        {
            playFishing = false;
            SetYarnVariable("$playFishing", false);

            if (GameManager.instance != null)
            {
                GameManager.instance.LoadFishing();
            }
        }
        vs.TryGetValue("$playCooking", out playCooking);
        if (playCooking)
        {
            playCooking = false;
            SetYarnVariable("$playCooking", false);

            if (GameManager.instance != null)
            {
                GameManager.instance.LoadCooking();
            }
        }
        vs.TryGetValue("$playLeaf", out playLeaf);
        if (playLeaf)
        {
            playLeaf = false;
            SetYarnVariable("$playLeaf", false);
            SetYarnVariable("$hasDoneLeaf", true);

            if (GameManager.instance != null)
            {
                GameManager.instance.LoadLeaf();
            }
        }
        vs.TryGetValue("$playSpotlight", out playSpotlight);
        if (playSpotlight)
        {
            playSpotlight = false;
            SetYarnVariable("$playSpotlight", false);
            SetYarnVariable("$hasDoneSpotlight", true);

            if (GameManager.instance != null)
            {
                GameManager.instance.LoadSpotlight();
            }
        }


        vs.TryGetValue("$talkedToNyrie", out talkedToNyrie);
        vs.TryGetValue("$talkedToTalia", out talkedToTalia);
        vs.TryGetValue("$talkedToRuby", out talkedToRuby);
        vs.TryGetValue("$talkedToPepper", out talkedToPepper);
        vs.TryGetValue("$talkedToPoppy", out talkedToPoppy);
        vs.TryGetValue("$talkedToMillie", out talkedToMillie);
        vs.TryGetValue("$talkedToWilson", out talkedToWilson);
        vs.TryGetValue("$talkedToLily", out talkedToLily);
        vs.TryGetValue("$talkedToAngler", out talkedToAngler);

        vs.TryGetValue("$peopleMet", out float peopleMetFloat);
        peopleMet = Mathf.RoundToInt(peopleMetFloat);
        vs.TryGetValue("$isDaytime", out isDaytime);

        vs.TryGetValue("$hasUnpacked", out hasUnpacked);
        vs.TryGetValue("$hasDoneIntro", out hasDoneIntro);
        vs.TryGetValue("$campfireStoryRead", out campfireStoryRead);

        vs.TryGetValue("$caughtByTeacher", out caughtByTeacher);
        vs.TryGetValue("$triggerSneakOut", out triggerSneakOut);
        vs.TryGetValue("$firstMeetingDone", out firstMeetingDone);
        vs.TryGetValue("$campfireDay2", out campfireDay2);

        vs.TryGetValue("$hasFished", out hasFished);
        vs.TryGetValue("$hasCooked", out hasCooked);
        vs.TryGetValue("$campfireDay3", out campfireDay3);
        vs.TryGetValue("$hasDoneLeaf", out hasDoneLeaf);

        vs.TryGetValue("$canUseCamera", out canUseCamera);
        vs.TryGetValue("$hasGotFlowers", out hasGotFlowers);
        vs.TryGetValue("$campfireDay4", out campfireDay4);
        vs.TryGetValue("$talkAfterFlowers", out talkAfterFlowers);
        vs.TryGetValue("$hasDoneSpotlight", out hasDoneSpotlight);
        vs.TryGetValue("$goToForestDay4", out goToForestDay4);
        if (goToForestDay4)
        {
            goToForestDay4 = false;
            SetYarnVariable("$goToForestDay4", false);

            if (GameManager.instance != null)
            {
                GameManager.instance.GoToForestScene();
            }
        }
        vs.TryGetValue("$endScene", out endScene);
        if (endScene)
        {
            endScene = false;
            SetYarnVariable("endScene", false);

            if (GameManager.instance != null)
            {
                GameManager.instance.LoadEndScene();
            }
        }
        vs.TryGetValue("$ernestThinking", out ernestThinking);
        if (ernestThinking)
        {
            ernestThinking = false;
            SetYarnVariable("ernestThinking", false);

            if (MainDialogueManager.instance != null)
            {
                MainDialogueManager.instance.ErnestThinking();
            }
        }
    }

    // called from other scripts to set variables
    public void SetYarnVariable(string variableName, object value)
    {
        switch (variableName)
        {
            case "$day": day = (int)value; break;
            case "$isDaytime": isDaytime = (bool)value; break;
            case "$playSolitaire": playSolitaire = (bool)value; break;
            case "$playRhythm": playRhythm = (bool)value; break;
            case "$playFishing": playFishing = (bool)value; break;
            case "$playCooking": playCooking = (bool)value; break;
            case "$playLeaf": playLeaf = (bool)value; break;
            case "$playSpotlight": playSpotlight = (bool)value; break;

            case "$talkedToNyrie": talkedToNyrie = (bool)value; break;
            case "$talkedToTalia": talkedToTalia = (bool)value; break;
            case "$talkedToRuby": talkedToRuby = (bool)value; break;
            case "$talkedToPepper": talkedToPepper = (bool)value; break;
            case "$talkedToPoppy": talkedToPoppy = (bool)value; break;
            case "$talkedToMillie": talkedToMillie = (bool)value; break;
            case "$talkedToWilson": talkedToWilson = (bool)value; break;
            case "$talkedToLily": talkedToLily = (bool)value; break;
            case "$talkedToAngler": talkedToAngler = (bool)value; break;
            case "$peopleMet": peopleMet = (int)value; break;

            case "$hasUnpacked": hasUnpacked = (bool)value; break;
            case "$hasDoneIntro": hasDoneIntro = (bool)value; break;
            case "$campfireStoryRead": campfireStoryRead = (bool)value; break;

            case "$caughtByTeacher": caughtByTeacher = (bool)value; break;
            case "$triggerSneakOut": triggerSneakOut = (bool)value; break;
            case "$firstMeetingDone": firstMeetingDone = (bool)value; break;
            case "$campfireDay2": campfireDay2 = (bool)value; break;

            case "$hasFished": hasFished = (bool)value; break;
            case "$hasCooked": hasCooked = (bool)value; break;
            case "$campfireDay3": campfireDay3 = (bool)value; break;
            case "$hasDoneLeaf": hasDoneLeaf = (bool)value; break;

            case "$canUseCamera": canUseCamera = (bool)value; break;
            case "$hasGotFlowers": hasGotFlowers = (bool)value; break;
            case "$campfireDay4": campfireDay4 = (bool)value; break;
            case "$talkAfterFlowers": talkAfterFlowers = (bool)value; break;
            case "$hasDoneSpotlight": hasDoneSpotlight = (bool)value; break;
            case "$goToForestDay4": goToForestDay4 = (bool)value; break;

            case "$endScene": endScene = (bool)value; break;
            case "$ernestThinking": ernestThinking = (bool)value; break;

            default:
                Debug.LogWarning("Variable name not recognized: " + variableName);
                return;

        }

        if (dialogueRunner != null)
        {
            var vs = dialogueRunner.VariableStorage;

            if (value is bool boolValue)
                vs.SetValue(variableName, boolValue);
            else if (value is int intValue)
                vs.SetValue(variableName, (float)intValue);
            else if (value is float floatValue)
                vs.SetValue(variableName, floatValue);
            else if (value is string stringValue)
                vs.SetValue(variableName, stringValue);
            else
                Debug.LogWarning($"Unsupported type {value.GetType()} for {variableName}");
        }
    }

    // --- helper methods for easy variable access ---
    public bool GetBool(string variableName)
    {
        switch (variableName)
        {
            case "$isDaytime": return isDaytime;
            case "$playSolitaire": return playSolitaire;
            case "$playRhythm": return playRhythm;
            case "$playFishing": return playFishing;
            case "$playCooking": return playCooking;
            case "$playLeaf": return playLeaf;
            case "$playSpotlight": return playSpotlight;

            case "$talkedToNyrie": return talkedToNyrie;
            case "$talkedToTalia": return talkedToTalia;
            case "$talkedToRuby": return talkedToRuby;
            case "$talkedToPepper": return talkedToPepper;
            case "$talkedToPoppy": return talkedToPoppy;
            case "$talkedToMillie": return talkedToMillie;
            case "$talkedToWilson": return talkedToWilson;
            case "$talkedToLily": return talkedToLily;
            case "$talkedToAngler": return talkedToAngler;

            case "$hasUnpacked": return hasUnpacked;
            case "$hasDoneIntro": return hasDoneIntro;
            case "$campfireStoryRead": return campfireStoryRead;

            case "$caughtByTeacher": return caughtByTeacher;
            case "$triggerSneakOut": return triggerSneakOut;
            case "$firstMeetingDone": return firstMeetingDone;
            case "$campfireDay2": return campfireDay2;

            case "$hasFished": return hasFished;
            case "$hasCooked": return hasCooked;
            case "$campfireDay3": return campfireDay3;
            case "$hasDoneLeaf": return hasDoneLeaf;

            case "$canUseCamera": return canUseCamera;
            case "$hasGotFlowers": return hasGotFlowers;
            case "$campfireDay4": return campfireDay4;
            case "$talkAfterFlowers": return talkAfterFlowers;
            case "$hasDoneSpotlight": return hasDoneSpotlight;
            case "$goToForestDay4": return goToForestDay4;

            case "$endScene": return endScene;
            case "$ernestThinking": return ernestThinking;

            default:
                Debug.LogWarning("Variable name not recognized: " + variableName);
                return false;
        }
        
    }

    public int GetInt(string variableName)
    {
        switch (variableName)
        {
            case "$day": return day;
            case "$peopleMet": return peopleMet;
            default:
                Debug.LogWarning("Variable name not recognized: " + variableName);
                return 0;
        }
    }

    public bool Has(string variableName)
    {
        return GetBool(variableName);
    }
}
