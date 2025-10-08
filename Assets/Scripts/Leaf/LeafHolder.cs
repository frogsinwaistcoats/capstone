using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LeafHolder : MonoBehaviour
{
    public static LeafHolder instance;

    public GameObject[] leafGroup;
    public GameObject[] leafGroupOutlines;

    public List<GameObject> leaves = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // ADD LATER    make sure the first group is selected for the first playthrough, then second group for the next playthrough, etc.

        int selectedGroup = Random.Range(0, leafGroup.Length - 1);

        leaves.AddRange(leafGroup[selectedGroup]
            .GetComponentsInChildren<Transform>(true) // gets all transforms (parent + children)
            .Where(t => t != leafGroup[selectedGroup].transform) // keep only children
            .Select(t => t.gameObject) // get the game object for each child
        );

        leafGroupOutlines[selectedGroup].SetActive(true);

        ShuffleLeaves();
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
}
