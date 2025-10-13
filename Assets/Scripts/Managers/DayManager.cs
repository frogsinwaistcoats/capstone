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

    public void StartNewDay()
    {
        dayCount++;
        UpdateDayText();

        //set yarn variable
        if (dialogueRunner != null)
        {
            dialogueRunner.VariableStorage.SetValue("$day", dayCount);
        }
    }

    public void UpdateDayText()
    {
        if (dayCounterText != null)
        {
            dayCounterText.text = "Day " + dayCount.ToString();
        }
    
    }

    private void OnDestroy()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
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
