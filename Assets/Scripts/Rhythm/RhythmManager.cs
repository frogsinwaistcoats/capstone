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

    public int currentScore;
    public int scorePerNote = 1;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI multiText;

    public int currentMultiplier;
    public int multiplierTracker;
    public int[] multiplierThresholds;

    public GameObject startButton;
    public GameObject endScreen;

    void Start()
    {
        AudioManager.instance.StopNightAudio();
        AudioManager.instance.StopDayAudio();

        instance = this;

        scoreText.text = "Score: 0";
        currentMultiplier = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Finished();
        }
    }

    public void NoteHit()
    {
        Debug.Log("Hit on time");

        if (currentMultiplier - 1 < multiplierThresholds.Length)
        {
            multiplierTracker++;

            if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;
            }
        }

        multiText.text = "Multiplier: x" + currentMultiplier;

        currentScore += scorePerNote * currentMultiplier;
        scoreText.text = "Score: " + currentScore;
    }

    public void NoteMissed()
    {
        Debug.Log("Missed note");

        currentMultiplier = 1;
        multiplierTracker = 0;
        multiText.text = "Multiplier: x" + currentMultiplier;
    }

    public void StartPlaying()
    {
        if (!startPlaying)
        {
            startButton.SetActive(false);
            startPlaying = true;
            beatScroller.hasStarted = true;

            music.Play();
            StartCoroutine(WaitForAudioToFinish());
        }
    }

    public void EndOfSong()
    {
        endScreen.SetActive(true);
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


