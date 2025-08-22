using UnityEngine;
using TMPro;
using System;

public class FishingButton : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private TextMeshPro text;
    [SerializeField] private KeyCode keyCode;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        text = GetComponentInChildren<TextMeshPro>();
    }

    private void Start()
    {
        keyCode = (KeyCode)Enum.Parse(typeof(KeyCode), text.text);
    }

    void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            Destroy(gameObject);
        }
    }
}
