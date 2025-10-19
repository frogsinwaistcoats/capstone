using UnityEngine;

public class DayTransition : MonoBehaviour
{
    private Animator animator;
    private bool isPlaying = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        gameObject.SetActive(false);
    }

    public void PlayTransition()
    {
        if (isPlaying) return;

        isPlaying = true;
        gameObject.SetActive(true);
        animator.Play("DayAnimation");
    }

    public void EndTransition()
    {
        isPlaying = false;
        gameObject.SetActive(false);
    }
}
