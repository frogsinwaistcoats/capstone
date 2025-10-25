using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LeafHolder : MonoBehaviour
{
    public static LeafHolder instance;

    public GameObject[] leafGroup;
    public GameObject[] leafGroupOutlines;

    public List<GameObject> leaves = new List<GameObject>();
    public List<GameObject> placedLeaves = new List<GameObject>();

    public GameObject instructionScreen;
    public GameObject winScreen;

    public bool hasStarted = false;
    public int totalLeaves;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
    }

    public void StartButton()
    {
        instructionScreen.SetActive(false);
        StartLeaf();
    }

    private void StartLeaf()
    {
        // Re-seed the random generator each time play starts
        Random.InitState(System.Environment.TickCount);

        int selectedGroup = Random.Range(0, leafGroup.Length);
        Debug.Log("Selected leaf group: " + selectedGroup);

        leaves.AddRange(leafGroup[selectedGroup]
            .GetComponentsInChildren<Transform>(true)
            .Where(t => t != leafGroup[selectedGroup].transform)
            .Select(t => t.gameObject)
        );

        leafGroupOutlines[selectedGroup].SetActive(true);
        totalLeaves = leaves.Count;

        ShuffleLeaves();
        hasStarted = true;
    }

    private void Update()
    {

        if (hasStarted)
        {
            if (placedLeaves.Count >= totalLeaves)
            {
                hasStarted = false;
                StartCoroutine(WaitAndShowWinScreen());
            }
                
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(WaitAndShowWinScreen());
        }
        
    }

    public IEnumerator WaitAndShowWinScreen()
    {
        yield return new WaitForSeconds(1f);
        winScreen.SetActive(true);
    }

    private void ShuffleLeaves()
    {
        for (int t = 0; t < leaves.Count; t++)
        {
            GameObject tmp = leaves[t];
            int r = Random.Range(t, leaves.Count);
            leaves[t] = leaves[r];
            leaves[r] = tmp;
        }
    }

    public void RemoveLeaf()
    {
        leaves.RemoveAt(0);
    }

    public void Finish()
    {
        GameManager.instance.GoToForestScene();
    }

    public void PlayAgain()
    {
        winScreen.SetActive(false);
        leaves.Clear();
        placedLeaves.Clear();

        StartLeaf();
    }
}
