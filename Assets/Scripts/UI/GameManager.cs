using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Yarn;
using Yarn.Unity;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private Vector3 lastPlayerPos;
    [SerializeField] private string previousScene;
    public bool hasUnpacked;
    public bool hasDoneIntro;

    private bool talkedToNyrie;
    private bool talkedToTalia;
    private bool talkedToRuby;
    private bool talkedToPepper;
    private bool talkedToPoppy;
    private bool talkedToMillie;
    private bool talkedToWilson;
    private bool talkedToLily;
    private bool talkedToAngler;

    public float peopleMet;
    public bool metEveryone;
    public bool campfireStoryRead;

    [Header("Day/Night Settings")]
    public bool isDaytime = true;
    public Material daySkybox;
    public Material nightSkybox;

    public Image dayIcon;
    public Sprite sunIcon;
    public Sprite moonIcon;


    private DialogueRunner dialogueRunner;

    private InMemoryVariableStorage variableStorage;


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
        dayIcon = GameObject.Find("---- UI ----/OtherCanvas/DayNightIndicator").GetComponent<Image>();
    }

    private void Update()
    {
        //set yarn variable
        if (dialogueRunner != null)
        {
            dialogueRunner.VariableStorage.SetValue("$hasUnpacked", hasUnpacked);

            if (!hasDoneIntro)
                dialogueRunner.VariableStorage.TryGetValue("$hasDoneIntro", out hasDoneIntro);

            if (!metEveryone)
                dialogueRunner.VariableStorage.TryGetValue("$peopleMet", out peopleMet);
                if (peopleMet >= 9)
                {
                    metEveryone = true;
                }

            dialogueRunner.VariableStorage.TryGetValue("$campfireStoryRead", out campfireStoryRead);
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
        dayIcon = GameObject.Find("---- UI ----/OtherCanvas/DayNightIndicator")?.GetComponent<Image>();
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadCampScene()
    {
        Debug.Log("Loading Camp Scene");
        previousScene = SceneManager.GetActiveScene().name;

        StartCoroutine(LoadSceneAndRun());
    }

    private IEnumerator LoadSceneAndRun()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("CampScene");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        if (previousScene == "Solitaire")
        {
            PlayerMovement.instance.transform.position = lastPlayerPos;
        }
        else if (previousScene == "Rhythm")
        {
            SetToNight();
            MainDialogueManager.instance.ErnestThoughts();
        }
    }

    public void LoadSolitaire()
    {
        previousScene = SceneManager.GetActiveScene().name;
        lastPlayerPos = FindAnyObjectByType<PlayerMovement>().transform.position;
        SceneManager.LoadScene("Solitaire");
    }

    public void LoadUnpacking()
    {
        previousScene = SceneManager.GetActiveScene().name;
        lastPlayerPos = FindAnyObjectByType<PlayerMovement>().transform.position;
        SceneManager.LoadScene("Unpacking");
    }

    public void LoadRhythm()
    {
        previousScene = SceneManager.GetActiveScene().name;
        lastPlayerPos = FindAnyObjectByType<PlayerMovement>().transform.position;
        SceneManager.LoadScene("Rhythm");
    }

    public void SetToDay()
    {
        // set to day
        Debug.Log("Setting to day");
        isDaytime = true;

        RenderSettings.skybox = daySkybox;
        DynamicGI.UpdateEnvironment();

        dayIcon = GameObject.Find("---- UI ----/OtherCanvas/DayNightIndicator")?.GetComponent<Image>();
        dayIcon.sprite = sunIcon;

        AudioManager.instance.StopNightAudio();
        AudioManager.instance.PlayDayAudio();
    }

    public void SetToNight()
    {
        // set to night
        Debug.Log("Setting to night");
        isDaytime = false;

        RenderSettings.skybox = nightSkybox;
        DynamicGI.UpdateEnvironment();

        dayIcon = GameObject.Find("---- UI ----/OtherCanvas/DayNightIndicator")?.GetComponent<Image>();
        dayIcon.sprite = moonIcon;

        AudioManager.instance.StopDayAudio();
        AudioManager.instance.PlayNightAudio();
    }
}
