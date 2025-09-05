using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class FishingManager : MonoBehaviour
{
    public static FishingManager instance;

    public GameObject button;
    public Canvas canvas;
    public float spawnDelay;

    public GameObject successScreen;
    public GameObject failScreen;

    [SerializeField] private List<GameObject> activeKeys = new List<GameObject>();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //Instantiate(button, canvas.transform);
        InvokeRepeating("SpawnButton", 0f, spawnDelay);
    }

    public void SpawnButton()
    {
        GameObject newKey = Instantiate(button);
        activeKeys.Add(newKey);
    }

    public IEnumerator EndGame()
    {
        CancelInvoke();
        yield return new WaitForSeconds(1f);
        successScreen.SetActive(true);
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
}
