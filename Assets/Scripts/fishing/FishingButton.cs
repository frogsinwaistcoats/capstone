using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using System.Collections;

public class FishingButton : MonoBehaviour
{
    [SerializeField] private Image buttonImage;
    [SerializeField] private KeyCode chosenKey;
    public KeyCode[] possibleKeys = 
        { KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D, KeyCode.E, KeyCode.F, 
        KeyCode.G, KeyCode.H, KeyCode.I, KeyCode.J, KeyCode.K, KeyCode.L, 
        KeyCode.M, KeyCode.N, KeyCode.O, KeyCode.P, KeyCode.Q, KeyCode.R, 
        KeyCode.S, KeyCode.T, KeyCode.U, KeyCode.V, KeyCode.W, KeyCode.X, 
        KeyCode.Y, KeyCode.Z };
    public Sprite[] possibleKeySprites;
    public Image buttonFill;
    public Transform targetPos;
    public float duration;
    public bool hasMissed;
    public Vector3 scale;

    private void Start()
    {
        int spawnPointX = UnityEngine.Random.Range(200, 1720);
        int spawnPointY = UnityEngine.Random.Range(100, 980);

        Vector2 spawnPos = new Vector2(spawnPointX, spawnPointY);
        transform.position = spawnPos;
        transform.localScale = scale;

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
        int randomIndex = UnityEngine.Random.Range(0, possibleKeys.Length);
        chosenKey = possibleKeys[randomIndex];
        buttonImage.sprite = possibleKeySprites[randomIndex];
        hasMissed = false;
        StartCoroutine(FillButton(targetPos, duration));
    }

    IEnumerator FillButton(Transform targetPos, float timeToMove)
    {
        Vector3 startPos = buttonFill.transform.position;
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / timeToMove;
            buttonFill.transform.position = Vector3.Lerp(startPos, targetPos.position, t);
            yield return null;
        }
        transform.position = targetPos.position;
        hasMissed = true;
        Fish.instance.MoveBack();
        StartCoroutine(DestroyKey());
    }

    IEnumerator DestroyKey()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
