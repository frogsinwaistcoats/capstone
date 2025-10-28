using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject loadScreen;

    public void PlayGame()
    {
        loadScreen.SetActive(true);
        loadScreen.GetComponent<Animator>().Play("Bus_Anim");
        AudioManager.instance.Play("Bus");
        StartCoroutine(GoToCampScene());
    }

    public IEnumerator GoToCampScene()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("CampScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    
}
