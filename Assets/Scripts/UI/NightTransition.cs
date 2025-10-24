using UnityEngine;

public class NightTransition : MonoBehaviour
{
    public static NightTransition instance;

    public Animator animator;
    private bool isPlaying = false;

    private void Awake()
    {
        instance = this;
    }

    public void PlayTransition()
    {
        if (isPlaying) return;

        isPlaying = true;
        animator.Play("NightAnimation");
    }

    public void EndTransition()
    {
        isPlaying = false;
        Destroy(gameObject);
    }
}
