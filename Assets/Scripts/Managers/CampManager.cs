using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class CampManager : MonoBehaviour
{
    public static CampManager instance;
    //public DayManager dayManager;
    public PlayerMovement player { get; set; }
    private GameObject quitConfirm;
    private GameObject controls;
    private GameObject resetPosButton;


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

        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            quitConfirm = GameObject.Find("---- UI ----/OtherCanvas/QuitConfirm");
            controls = GameObject.Find("---- UI ----/OtherCanvas/Controls");
            resetPosButton = GameObject.Find("---- UI ----/OtherCanvas/Reset");

            quitConfirm.SetActive(true);
            controls.SetActive(false);
            resetPosButton.SetActive(false);
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Return()
    {
        Debug.Log("clicked return");

        quitConfirm = GameObject.Find("---- UI ----/OtherCanvas/QuitConfirm");
        controls = GameObject.Find("---- UI ----/OtherCanvas/Controls");
        resetPosButton = GameObject.Find("---- UI ----/OtherCanvas/Reset");

        quitConfirm.SetActive(false);
        controls.SetActive(true);
        resetPosButton.SetActive(true);
    }
}

