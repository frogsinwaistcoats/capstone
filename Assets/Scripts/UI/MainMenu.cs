using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private PlayableDirector playableDirector;

    public void PlayGame()
    {
        playableDirector.Play();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    
}
