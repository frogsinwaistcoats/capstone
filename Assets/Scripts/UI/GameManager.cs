using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn;
using Yarn.Unity;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private Vector3 lastPlayerPos;
    private Scene previousScene;
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
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadCampScene()
    {
        Debug.Log("Loading Camp Scene");
        previousScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene("CampScene");
        if (previousScene.name == "Solitaire")
        {
            PlayerMovement.instance.transform.position = lastPlayerPos;
        }

        
    }

    public void LoadSolitaire()
    {
        previousScene = SceneManager.GetActiveScene();
        lastPlayerPos = FindAnyObjectByType<PlayerMovement>().transform.position;
        SceneManager.LoadScene("Solitaire");
    }

    public void LoadUnpacking()
    {
        previousScene = SceneManager.GetActiveScene();
        lastPlayerPos = FindAnyObjectByType<PlayerMovement>().transform.position;
        SceneManager.LoadScene("Unpacking");
    }

    public void LoadRhythm()
    {
        previousScene = SceneManager.GetActiveScene();
        lastPlayerPos = FindAnyObjectByType<PlayerMovement>().transform.position;
        SceneManager.LoadScene("Rhythm");
    }

}
