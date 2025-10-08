using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CookingManager : MonoBehaviour
{
    public static CookingManager instance;

    public GameObject[] foodGroup;
    public GameObject[] board;

    public List<GameObject> food = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;

        int selectedGroup = Random.Range(0, foodGroup.Length - 1);

        foodGroup[selectedGroup].SetActive(true);
        board[selectedGroup].SetActive(true);

        food.AddRange(
            foodGroup[selectedGroup].transform.Cast<Transform>()
            .Select(t => t.gameObject)
        );
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResetFoodPos();
        }

    }

    public void ResetFoodPos()
    {
        foreach (GameObject food in food)
        {
            food.GetComponent<CookingFood>().ReturnToBoard();
        }
    }
}
