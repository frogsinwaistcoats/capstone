using UnityEngine;
using TMPro;
using Yarn.Unity;
using UnityEngine.SceneManagement;
using System.Collections;

public class DayManager : MonoBehaviour//, IDataPersistence
{
    public static DayManager instance;

    public int dayCount { get; private set; } = 1;
    public TextMeshProUGUI dayCounterText;
    private DialogueRunner dialogueRunner;



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

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        dialogueRunner = FindFirstObjectByType<DialogueRunner>();

        if(dayCounterText == null)
        {
            GameObject textObject = GameObject.Find("---- UI ----/OtherCanvas/DayCounterText");
            if(textObject != null)
            {
                dayCounterText = textObject.GetComponent<TextMeshProUGUI>();
            }
        }

        UpdateDayText();
    }

    public void StartNewDay(int newDayCount)
    {
        dayCount = newDayCount;
        UpdateDayText();

        //set yarn variable
        if (LoadYarnVariables.instance != null)
        {
            LoadYarnVariables.instance.SetYarnVariable("$day", dayCount);
        }

        var questList = FindFirstObjectByType<QuestsList>();
        if (questList != null)
        {
            StartCoroutine(questList.StartQuestsNextFrame());
        }
    }

    public void UpdateDayText()
    {
        if (dayCounterText != null)
        {
            if (dayCount == 1)
                dayCounterText.text = "Monday";
            else if (dayCount == 2)
                dayCounterText.text = "Tuesday";
            else if (dayCount == 3)
                dayCounterText.text = "Wednesday";
            else if (dayCount == 4)
                dayCounterText.text = "Thursday";
            else if (dayCount == 5)
                dayCounterText.text = "Friday";
        }
    
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(InitialiseSceneObjects());
    }

    private IEnumerator InitialiseSceneObjects()
    {
        yield return null;

        dialogueRunner = FindFirstObjectByType<DialogueRunner>();

        if (dayCounterText == null)
        {
            GameObject textObject = GameObject.Find("---- UI ----/OtherCanvas/DayCounterText");
            if (textObject != null)
            {
                dayCounterText = textObject.GetComponent<TextMeshProUGUI>();
            }
        }

        if (dayCounterText != null)
        {
            UpdateDayText();
        }
    }
}
