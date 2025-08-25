using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using System.Collections;

public class FishingButton : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private TextMeshProUGUI keyText;
    [SerializeField] private KeyCode chosenKey;
    public KeyCode[] possibleKeys = 
        { KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D, KeyCode.E, KeyCode.F, 
        KeyCode.G, KeyCode.H, KeyCode.I, KeyCode.J, KeyCode.K, KeyCode.L, 
        KeyCode.M, KeyCode.N, KeyCode.O, KeyCode.P, KeyCode.Q, KeyCode.R, 
        KeyCode.S, KeyCode.T, KeyCode.U, KeyCode.V, KeyCode.W, KeyCode.X, 
        KeyCode.Y, KeyCode.Z };

    public Image buttonFill;
    public Transform targetPos;
    public float duration;
    public bool isFilling;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        keyText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        GenerateRandomKeyCode();
    }

    void Update()
    {
        if (Input.GetKeyDown(chosenKey))
        {
            Destroy(gameObject);
        }

        if (isFilling)
        {
            
        }
        
    }

    void GenerateRandomKeyCode()
    {
        int randomIndex = UnityEngine.Random.Range(0, possibleKeys.Length);
        chosenKey = possibleKeys[randomIndex];
        keyText.text = chosenKey.ToString();
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
    }
}
