using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class RhythmManager : MonoBehaviour
{
    public static RhythmManager instance;
    public BeatScroller beatScroller;

    public AudioSource music;
    public bool startPlaying;

    public int currentScore = 0;
    public int scorePerNote = 1;
    public int missCount = 0;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI streakText;
    public TextMeshProUGUI missedText;

    public int currentStreak = 0;
    public int highestStreak = 0;

    public GameObject instructionScreen;
    public GameObject startPrompt;
    public GameObject winScreen;

    public bool canPlay;

    bool dPressed = false;
    bool fPressed = false;
    bool jPressed = false;
    bool kPressed = false; 

    void Start()
    {
        //AudioManager audioManager = FindFirstObjectByType<AudioManager>();
        //audioManager.StopNightAudio();
        //audioManager.StopDayAudio();
        canPlay = false;

        instance = this;

        scoreText.text = "Score: 0";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
            dPressed = true;
        if (Input.GetKeyUp(KeyCode.D))
            dPressed = false;

        if (Input.GetKeyDown(KeyCode.F))
            fPressed = true;
        if (Input.GetKeyUp(KeyCode.F))
            fPressed = false;

        if (Input.GetKeyDown(KeyCode.J))
            jPressed = true;
        if (Input.GetKeyUp(KeyCode.J))
            jPressed = false;

        if (Input.GetKeyDown(KeyCode.K))
            kPressed = true;
        if (Input.GetKeyUp(KeyCode.K))
            kPressed = false;

        if (dPressed && fPressed && jPressed && kPressed)
        {
            StartPlaying();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Finished();
        }
    }

    public void NoteHit()
    {
        Debug.Log("Hit on time");

        currentScore += scorePerNote;
        currentStreak += scorePerNote;
        scoreText.text = "Hits: " + currentScore;
        streakText.text = "Streak: " + currentStreak;
    }

    public void NoteMissed()
    {
        Debug.Log("Missed note");

        missCount++;
        missedText.text = "Miss: " + missCount;
        currentStreak = 0;
        streakText.text = "Streak: " + currentStreak;
    }

    public void CloseInstructions()
    {
        instructionScreen.SetActive(false);
        canPlay = true;
        startPrompt.SetActive(true);
    }

    public void StartPlaying()
    {
        if (canPlay)
        {
            startPrompt.SetActive(false);

            if (!startPlaying)
            {
                startPlaying = true;
                beatScroller.hasStarted = true;

                music.Play();
                StartCoroutine(WaitForAudioToFinish());
            }
        }
    }

    public void EndOfSong()
    {
        winScreen.SetActive(true);
    }

    public void Finished()
    {
        GameManager.instance.LoadCampScene();
    }

    IEnumerator WaitForAudioToFinish()
    {
        yield return new WaitUntil(() => !music.isPlaying);
        EndOfSong();
    }
}


