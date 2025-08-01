using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager current;


    private void Start()
    {
        current = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void QuitGame()
    {
        Application.Quit();
    }

}
