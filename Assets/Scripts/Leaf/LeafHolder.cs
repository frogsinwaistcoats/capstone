using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class LeafHolder : MonoBehaviour
{
    public static LeafHolder instance;

    public List<GameObject> leaves = new List<GameObject>();

    public Transform slotOne;
    public Transform slotTwo;
    public Transform slotThree;
    public Transform slotFour;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
