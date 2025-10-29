using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using System.Collections;

public class FishingButton : MonoBehaviour
{
    [SerializeField] private KeyCode chosenKey;
    public KeyCode[] possibleKeys = 
        { KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D, KeyCode.E, KeyCode.F, 
        KeyCode.G, KeyCode.H, KeyCode.I, KeyCode.J, KeyCode.K, KeyCode.L, 
        KeyCode.M, KeyCode.N, KeyCode.O, KeyCode.P, KeyCode.Q, KeyCode.R, 
        KeyCode.S, KeyCode.T, KeyCode.U, KeyCode.V, KeyCode.W, KeyCode.X, 
        KeyCode.Y, KeyCode.Z };
    public AnimationClip[] possibleKeyAnims;
    public bool hasMissed;
    public Animator animator;

    private void Start()
    { 
        GenerateRandomKeyCode();
    }

    void Update()
    {
        if (!hasMissed && Input.GetKeyDown(chosenKey))
        {
            Destroy(gameObject);
            Fish.instance.MoveForward();
        }
    }

    void GenerateRandomKeyCode()
    {
        int randomIndex = UnityEngine.Random.Range(0, possibleKeys.Length - 1);
        chosenKey = possibleKeys[randomIndex];
        animator.Play(possibleKeyAnims[randomIndex].name);

        hasMissed = false;
        StartCoroutine(TrackButton());
    }

    
    IEnumerator TrackButton()
    {
        yield return new WaitForSeconds(possibleKeyAnims[0].length);
        hasMissed = true;
        Fish.instance.MoveBack();
        Destroy(gameObject);
    }
}
