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

    public Vector3 forestPlayerPos;

    [Header("Day/Night Settings")]
    public bool isDaytime = true;
    public Material daySkybox;
    public Material nightSkybox;

    public Image dayIcon;
    public Sprite sunIcon;
    public Sprite moonIcon;

    private DialogueRunner dialogueRunner;

    public bool hasPlayedSolitaire = false;

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

    private void Start()
    {
        dialogueRunner = FindFirstObjectByType<DialogueRunner>();
        dayIcon = GameObject.Find("---- UI ----/OtherCanvas/DayNightIndicator")?.GetComponent<Image>();
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

    // ------- Scene Loading -------

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadCampScene(System.Action onLoaded = null)
    {
        Debug.Log("Loading Camp Scene");
        previousScene = SceneManager.GetActiveScene().name;
        StartCoroutine(LoadSceneAndRun(onLoaded));
    }

    private IEnumerator LoadSceneAndRun(System.Action onLoaded)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("CampScene");
        while (!asyncLoad.isDone)
            yield return null;

        onLoaded?.Invoke();

        if (previousScene == "Solitaire")
        {
            PlayerMovement.instance.transform.position = lastPlayerPos;
            PlayerMovement.instance.canMove = true;
        }
        else if (previousScene == "Rhythm")
        {
            SetToNight();
            LoadYarnVariables.instance.SetYarnVariable("$campfireStoryRead", true);
            MainDialogueManager.instance.ErnestDayOne();
        }
        else if (previousScene == "Unpacking")
        {
            PlayerMovement.instance.transform.position = lastPlayerPos;
            PlayerMovement.instance.canMove = true;
        }
    }

    public void LoadSolitaire()
    {
        Debug.Log("playing solitaire");
        if (!hasPlayedSolitaire)
            hasPlayedSolitaire = true;
        LoadYarnVariables.instance.SetYarnVariable("$playSolitaire", false);
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
        
        //LoadYarnVariables.instance.SetYarnVariable("$campfireStoryRead", true);

        SceneManager.LoadScene("Rhythm");
    }

    public void LoadSneaking()
    {
        previousScene = SceneManager.GetActiveScene().name;
        lastPlayerPos = FindAnyObjectByType<PlayerMovement>().transform.position;
        SceneManager.LoadScene("Sneaking");
    }

    public void GoToForestScene()
    {
        LoadCampScene(() =>
        {
            PlayerMovement.instance.transform.position = forestPlayerPos;

            if (DayManager.instance.dayCount == 2)
            {
                MainDialogueManager.instance.FirstMeetingDialogue();
            }
        });
    }

    // ------- Day/Night -------

    public void SetToDay()
    {
        Debug.Log("Setting to day");
        isDaytime = true;
        LoadYarnVariables.instance.SetYarnVariable("$isDaytime", true);

        RenderSettings.skybox = daySkybox;
        DynamicGI.UpdateEnvironment();

        dayIcon = GameObject.Find("---- UI ----/OtherCanvas/DayNightIndicator")?.GetComponent<Image>();
        if (dayIcon != null)
            dayIcon.sprite = sunIcon;

        AudioManager.instance.StopNightAudio();
        AudioManager.instance.PlayDayAudio();
    }

    public void SetToNight()
    {
        // set to night
        Debug.Log("Setting to night");
        isDaytime = false;
        LoadYarnVariables.instance.SetYarnVariable("$isDaytime", false);

        RenderSettings.skybox = nightSkybox;
        DynamicGI.UpdateEnvironment();

        dayIcon = GameObject.Find("---- UI ----/OtherCanvas/DayNightIndicator")?.GetComponent<Image>();
        if (dayIcon != null)
            dayIcon.sprite = moonIcon;

        AudioManager.instance.StopDayAudio();
        AudioManager.instance.PlayNightAudio();
    }

    // ------- Sleep Conditions -------

    public bool CanSleep()
    {
        int dayCount = DayManager.instance.dayCount;

        if (dayCount == 1)
        {
            bool hasUnpacked = LoadYarnVariables.instance.GetBool("$hasUnpacked");
            bool campfireStoryRead = LoadYarnVariables.instance.GetBool("$campfireStoryRead");
            int peopleMet = LoadYarnVariables.instance.GetInt("$peopleMet");
            bool metEveryone = peopleMet >= 9;

            return hasUnpacked && metEveryone && campfireStoryRead;
        }

        // add other days here
        return false;
    }
}
