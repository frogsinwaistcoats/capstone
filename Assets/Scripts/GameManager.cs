using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public DayManager dayManager;
    public PlayerMovement player { get; set; }
    private GameObject quitConfirm;
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        quitConfirm = GameObject.Find("---- UI ----/OtherCanvas/QuitConfirm");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            quitConfirm.SetActive(true);
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Return()
    {
        quitConfirm.SetActive(false);
    }
}

