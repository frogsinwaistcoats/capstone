using UnityEngine;
using UnityEngine.InputSystem;

public class UnpackingItem : MonoBehaviour
{
    [SerializeField] Camera mainCamera;

    private void Start()
    {
        mainCamera = FindAnyObjectByType<Camera>();
    }

    private void Update()
    {
        Debug.Log(Input.mousePosition);
    }
}
