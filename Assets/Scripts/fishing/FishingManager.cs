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

    public Vector2 normMin = new Vector2(0.45f, 0.2f);
    public Vector2 normMax = new Vector2(0.85f, 0.4f);

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
        InvokeRepeating("SpawnButton", 0f, spawnDelay);
    }

    public void SpawnButton()
    {
        //Vector2 rnd = new Vector2(
        //Random.Range(normMin.x, normMax.x),
        //Random.Range(normMin.y, normMax.y)
        //);

        //// Convert normalized -> pixel
        //Vector2 screenPos = new Vector2(
        //    rnd.x * Screen.width,
        //    rnd.y * Screen.height
        //);

        //// Convert pixel -> world
        //Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        //worldPos.z = 0f;

        int spawnPointX = Random.Range(-7, 15);
        int spawnPointY = Random.Range(-8, -6);

        Debug.Log("X " + spawnPointX + ", Y " + spawnPointY);

        Vector3 spawnPos = new Vector3(spawnPointX, spawnPointY, 0f);


        GameObject newButton = Instantiate(button, spawnPos, Quaternion.identity);
        activeKeys.Add(newButton);

        Debug.Log("Spawned at: " + newButton.transform.position);
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
