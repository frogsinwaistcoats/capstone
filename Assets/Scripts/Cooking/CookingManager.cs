using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CookingManager : MonoBehaviour
{
    public static CookingManager instance;

    public GameObject instructionScreen;
    public GameObject chooseOrderScreen;
    public GameObject winScreen;

    public GameObject[] foodGroup;
    public GameObject[] board;
    public GameObject[] orders;

    public List<GameObject> food = new List<GameObject>();

    public int selectedFoodGroup;

    private void Awake()
    {
        instance = this;
        instructionScreen.SetActive(true);
    }

    public void StartButton()
    {
        instructionScreen.SetActive(false);
        chooseOrderScreen.SetActive(true);
    }

    // choose students
    public void ChooseAngler()
    {
        StartCooking(0);
    }

    public void ChooseLily()
    {
        StartCooking(1);
    }

    public void ChooseMillie()
    {
        StartCooking(2);
    }
    public void ChoosePepper()
    {
        StartCooking(3);
    }

    public void ChoosePoppy()
    {
        StartCooking(4);
    }
    public void ChooseRuby()
    {
        StartCooking(5);
    }
    public void ChooseTalia()
    {
        StartCooking(6);
    }

    public void ChooseRandom()
    {
        int selectedGroup = Random.Range(0, foodGroup.Length);
        StartCooking(selectedGroup);
    }

    // end of students

    void StartCooking(int selectedGroup)
    {
        chooseOrderScreen.SetActive(false);
        foodGroup[selectedGroup].SetActive(true);
        board[selectedGroup].SetActive(true);
        orders[selectedGroup].SetActive(true);
        selectedFoodGroup = selectedGroup;

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

    public void CheckForWin()
    {
        Debug.Log("Check for win");
        foreach (GameObject food in food)
        {
            CookingFood foodScript = food.GetComponent<CookingFood>();
            
            if (!foodScript.isOnBoard)
            {
                return;
            }            
        }
        StartCoroutine(Win());
    }

    public IEnumerator Win()
    {
        yield return new WaitForSeconds(0.5f);
        orders[selectedFoodGroup].SetActive(false);
        winScreen.SetActive(true);
    }

    public void PlayAgain()
    {
        winScreen.SetActive(false);
        ResetFoodPos();
        food.Clear();
        foodGroup[selectedFoodGroup].SetActive(false);
        board[selectedFoodGroup].SetActive(false);
        instructionScreen.SetActive(true);
    }

    public void ReturnToCamp()
    {
        GameManager.instance.LoadCampScene();
    }
}
