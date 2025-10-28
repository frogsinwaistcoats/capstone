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
    public GameObject winScreen2;

    public bool hasStarted = false;
    public int totalLeaves;

    public int selectedGroup;
    public bool hasPlayed = false;
    public bool hasPlayedBoth = false;

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
        if (!hasPlayed)
        {
            selectedGroup = 0;
        }
        else if (hasPlayed)
        {
            selectedGroup = 1;
            hasPlayedBoth = true;
        }

            leaves.AddRange(leafGroup[selectedGroup]
                .GetComponentsInChildren<Transform>(true)
                .Where(t => t != leafGroup[selectedGroup].transform)
                .Select(t => t.gameObject)
            );

        leafGroupOutlines[selectedGroup].SetActive(true);
        totalLeaves = leaves.Count;

        ShuffleLeaves();
        hasStarted = true;
        hasPlayed = true;
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
        
    }

    public IEnumerator WaitAndShowWinScreen()
    {
        yield return new WaitForSeconds(1f);

        if (!hasPlayedBoth)
        {
            winScreen.SetActive(true);
        }
        else if (hasPlayedBoth)
        {
            winScreen2.SetActive(true);
        }
        

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

        foreach (var leaf in placedLeaves)
        {
            leaf.gameObject.SetActive(false);
        }

        leaves.Clear();
        placedLeaves.Clear();

        StartLeaf();
    }
}
