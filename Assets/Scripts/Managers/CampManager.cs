using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class CampManager : MonoBehaviour
{
    public static CampManager instance;
    //public DayManager dayManager;
    public PlayerMovement player { get; set; }
    public GameObject quitConfirm;

    private bool isActive;

    public InventoryManager inventoryManager;


    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isActive && !inventoryManager.menuActivated)
        {
            quitConfirm.SetActive(true);
            isActive = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isActive && !inventoryManager.menuActivated)
        {
            Return();
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            Return();
        }

    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Return()
    {
        Debug.Log("clicked return");

        quitConfirm = GameObject.Find("---- UI ----/OtherCanvas/QuitConfirm");

        quitConfirm.SetActive(false);
        isActive = false;
    }
}
