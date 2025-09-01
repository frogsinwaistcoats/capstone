using TMPro;
using UnityEngine;

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

    void Start()
    {
        instance = this;

        scoreText.text = "Score: 0";
        currentMultiplier = 1;
    }

    void Update()
    {
        if (!startPlaying)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                startPlaying = true;
                beatScroller.hasStarted = true;

                music.Play();
            }
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
}
