using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Yarn;
using Yarn.Unity;
using static QuestsList;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private Vector3 lastPlayerPos;
    [SerializeField] private string previousScene;

    [Header("Day/Night Settings")]
    public bool isDaytime = true;
    public Material daySkybox;
    public Material nightSkybox;

    public Image dayIcon;
    public Sprite sunIcon;
    public Sprite moonIcon;

    private DialogueRunner dialogueRunner;

    public bool hasPlayedSolitaire = false;

    public NightTransition nightTransitionPrefab;

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

    private void Update()
    {
        if (DayManager.instance.dayCount == 2)
        {
            if (LoadYarnVariables.instance.GetBool("$campfireStoryRead"))
            {
                LoadYarnVariables.instance.SetYarnVariable("$campfireStoryRead", false);
            }
        }
    }

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
            
            if (DayManager.instance.dayCount == 1)
            {
                SetToNight(false);
                LoadYarnVariables.instance.SetYarnVariable("$campfireStoryRead", true);
                MainDialogueManager.instance.ErnestDayOne();
            }
            else if (DayManager.instance.dayCount == 2)
            {
                SetToNight(true);
                MainDialogueManager.instance.SneakingOutDialogue();
            }
            else if (DayManager.instance.dayCount == 3)
            {
                SetToNight(true);                
                MainDialogueManager.instance.DayThreeSneakingPrompt();
            }
           
        }
        else if (previousScene == "Unpacking")
        {
            PlayerMovement.instance.transform.position = lastPlayerPos;
            PlayerMovement.instance.canMove = true;
        }
        else if (previousScene == "Fishing")
        {
            PlayerMovement.instance.transform.position = lastPlayerPos;
            PlayerMovement.instance.canMove = true;
            LoadYarnVariables.instance.SetYarnVariable("$hasFished", true);
        }
        else if (previousScene == "Cooking")
        {
            PlayerMovement.instance.transform.position = lastPlayerPos;
            PlayerMovement.instance.canMove = true;
            LoadYarnVariables.instance.SetYarnVariable("$hasCooked", true);
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

    public void LoadFishing()
    {
        previousScene = SceneManager.GetActiveScene().name;
        lastPlayerPos = FindAnyObjectByType<PlayerMovement>().transform.position;

        SceneManager.LoadScene("Fishing");
    }

    public void LoadCooking()
    {
        previousScene = SceneManager.GetActiveScene().name;
        lastPlayerPos = FindAnyObjectByType<PlayerMovement>().transform.position;

        SceneManager.LoadScene("Cooking");
    }

    public void LoadLeaf()
    {
        previousScene = SceneManager.GetActiveScene().name;
        lastPlayerPos = FindAnyObjectByType<PlayerMovement>().transform.position;

        LoadYarnVariables.instance.SetYarnVariable("$hasDoneLeaf", true);

        SceneManager.LoadScene("Leaf");

    }

    public void LoadSpotlight()
    {
        previousScene = SceneManager.GetActiveScene().name;
        lastPlayerPos = FindAnyObjectByType<PlayerMovement>().transform.position;

        SceneManager.LoadScene("Spotlight");
    }

    public void GoToForestScene()
    {
        LoadCampScene(() =>
        {
            PlayerMovement.instance.GoToForestPos();

            if (DayManager.instance.dayCount == 2)
            {
                SetToNight(false);
                MainDialogueManager.instance.FirstMeetingDialogue();
            }
            if (DayManager.instance.dayCount == 3)
            {
                SetToNight(false);
                if (previousScene != "Leaf")
                {
                    MainDialogueManager.instance.SecondMeetingDialogue();
                }
                else if (previousScene == "Leaf")
                {
                    MainDialogueManager.instance.AfterLeafDialogue();
                }


            }
        });
    }

    public void ReturnToCampFromForest()
    {
        PlayerMovement.instance.GoToCampPos();
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
        CampBorder.instance.EnableSolidCollider();
    }

    public void SetToNight(bool playTransition)
    {
        // set to night
        Debug.Log("Setting to night");
        isDaytime = false;
        LoadYarnVariables.instance.SetYarnVariable("$isDaytime", false);


        if (playTransition == true)
        {
            GameObject canvas = GameObject.Find("---- UI ----/TransitionCanvas");
            NightTransition nightTransition = Instantiate(nightTransitionPrefab, canvas.transform);
            nightTransitionPrefab.PlayTransition();
        }

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
        else if (dayCount == 2)
        {
            
            bool hasMetAlex = LoadYarnVariables.instance.GetBool("$firstMeetingDone");
            return hasMetAlex && !isDaytime;
        }
        else if (dayCount == 3)
        {
            bool hasDoneLeaf = LoadYarnVariables.instance.GetBool("$hasDoneLeaf");
            return hasDoneLeaf && !isDaytime;
        }
        else if (dayCount == 4)
        {

        }

            // add other days here
            return false;
    }
}
