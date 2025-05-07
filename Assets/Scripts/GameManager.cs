using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager current;
    public int currentDay = 1;
    public TextMeshProUGUI dayCounterText;


    private void Awake()
    {
        if (current == null)
        {
            Debug.Log("GameManager Set Up");
            current = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("Duplicate GameManager Destroyed");
            Destroy(gameObject);
        }

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
    }

    public void StartNewDay()
    {
        currentDay++;
        UpdateDayText();
    }

    public void UpdateDayText()
    {
        dayCounterText.text = "Day " + currentDay.ToString();
    }
}
