using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishingManager : MonoBehaviour
{
    public static FishingManager instance;

    public GameObject button;
    public Canvas canvas;
    public float spawnDelay;

    public GameObject instructionScreen;
    public GameObject winScreen;
    public GameObject failScreen;

    public Sprite[] fishSprites;
    public Image fishImage;

    [SerializeField] private List<GameObject> activeKeys = new List<GameObject>();

    private void Awake()
    {
        instance = this;
    }

    public void StartButton()
    {
        instructionScreen.SetActive(false);
        StartFishing();
    }

    private void StartFishing()
    {
        //Instantiate(button, canvas.transform);
        InvokeRepeating("SpawnButton", 0f, spawnDelay);
    }

    public void SpawnButton()
    {
        GameObject newKey = Instantiate(button);
        activeKeys.Add(newKey);
    }

    public IEnumerator WinGame()
    {
        CancelInvoke();
        yield return new WaitForSeconds(1f);
        winScreen.SetActive(true);
        fishImage.sprite = fishSprites[Random.Range(0, fishSprites.Length)];
    }

    public IEnumerator FailGame()
    {
        CancelInvoke();
        foreach (GameObject key in activeKeys)
        {
            Destroy(key);
        }
        yield return new WaitForSeconds(1f);
        failScreen.SetActive(true);
    }

    public void ReturnToCamp()
    {
        GameManager.instance.LoadCampScene();
    }

    public void PlayAgain()
    {
        winScreen.SetActive(false);
        failScreen.SetActive(false);

        foreach (GameObject key in activeKeys)
        {
            if (key != null) Destroy(key);
        }
        activeKeys.Clear();

        Fish.instance.ResetBobber();
        StartFishing();
    }
}
