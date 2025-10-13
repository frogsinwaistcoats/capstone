using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameTest : MonoBehaviour
{
    public GameObject instructions;
    public bool instructionOpen = false;

    public void Unpacking()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Unpacking");
    }

    public void SneakingOut()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Sneaking");
    }

    public void Rhythm()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Rhythm");
    }

    public void HomePage()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("TestMinigames");
    }

    public void Instructions()
    {
        if (instructionOpen)
        {
            instructions.SetActive(false);
            instructionOpen = false;
        }
        else
        {
            instructions.SetActive(true);
            instructionOpen = true;
        }
    }
}
