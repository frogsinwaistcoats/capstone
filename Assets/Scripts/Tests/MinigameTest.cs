using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameTest : MonoBehaviour
{
    public GameObject instructions;
    public bool instructionOpen = false;

    public void Unpacking()
    {
        SceneManager.LoadScene("Unpacking");
    }

    public void SneakingOut()
    {
        SceneManager.LoadScene("Sneaking");
    }

    public void Rhythm()
    {
        SceneManager.LoadScene("Rhythm");
    }

    public void Cooking()
    {
        SceneManager.LoadScene("Cooking");
    }

    public void Leaf()
    {
        SceneManager.LoadScene("Leaf");
    }

    public void HomePage()
    {
        SceneManager.LoadScene("TestMinigames");
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
