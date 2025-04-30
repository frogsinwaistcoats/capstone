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
        dayCounterText.text = "Day " + currentDay.ToString();
    }
}
