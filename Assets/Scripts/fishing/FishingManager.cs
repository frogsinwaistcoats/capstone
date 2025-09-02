using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class FishingManager : MonoBehaviour
{
    public static FishingManager instance;

    public GameObject button;
    public Canvas canvas;
    public float spawnDelay;

    public GameObject successScreen;

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
        Instantiate(button, canvas.transform);
    }

    public void EndGame()
    {
        CancelInvoke();
        successScreen.SetActive(true);
    }
}
