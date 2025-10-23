using Unity.VisualScripting;
using UnityEngine;

public class LeafSlot : MonoBehaviour
{
    public bool isFilled = true;
    public bool isDone = false;

    LeafHolder leafHolder;

    private void Start()
    {
        leafHolder = LeafHolder.instance;
    }

    private void Update()
    {
        if (!isFilled)
        {
            SpawnLeaf();
        }
    }

    public void SpawnLeaf()
    {
        if (leafHolder.leaves.Count > 0)
        {
            isFilled = true;
            leafHolder.leaves[0].SetActive(true);
            leafHolder.leaves[0].transform.position = transform.position;
            leafHolder.leaves[0].GetComponent<Leaf>().leafSlot = this;
            leafHolder.RemoveLeaf();
        }
    }
}
