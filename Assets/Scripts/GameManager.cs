using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int currentDay = 1;
    public TextMeshProUGUI dayCounterText;

    private DialogueRunner dialogueRunner;

    public PlayerMovement player { get; set; }
    

    private void Awake()
    {
        if (instance == null)
        {
            Debug.Log("GameManager Set Up");
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("Duplicate GameManager Destroyed");
            Destroy(gameObject);
        }

        dialogueRunner = FindFirstObjectByType<DialogueRunner>();

        if (dayCounterText == null)
        {
            GameObject textObject = GameObject.Find("---- UI ----/OtherCanvas/DayCounterText");
            if (textObject != null)
            {
                dayCounterText = textObject.GetComponent<TextMeshProUGUI>();
                UpdateDayText();
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SaveSystem.Save();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SaveSystem.Load();
        }
    }

    public void StartNewDay()
    {
        currentDay++;
        UpdateDayText();

        //set yarn variable
        if(dialogueRunner != null)
        {
            dialogueRunner.VariableStorage.SetValue("$day", currentDay);
        }
    }

    public void UpdateDayText()
    {
        dayCounterText.text = "Day " + currentDay.ToString();
    }
}

