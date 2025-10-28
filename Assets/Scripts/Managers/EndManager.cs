using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndManager : MonoBehaviour
{
    public GameObject loadScreen;
    public GameObject black;

    public void Start()
    {
        loadScreen.GetComponent<Animator>().Play("BusEnd_Anim");
        StartCoroutine(GoToEndScene());
    }

    public IEnumerator GoToEndScene()
    {
        yield return new WaitForSeconds(4f);
        loadScreen.SetActive(false);
        black.SetActive(false);
    }

    public void ReturnToTitle()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
