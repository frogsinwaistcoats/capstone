using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class CookingGrids : MonoBehaviour
{
    public bool isAvailable = true;

    private void Update()
    {
        if (isAvailable)
        {
            ChangeAlpha1();
        }
        else
        {
            ChangeAlpha2();
        }
    }

    private void ChangeAlpha1()
    {
        Color tmp = GetComponent<SpriteRenderer>().color;
        tmp.a = 1f;
        GetComponent<SpriteRenderer>().color = tmp;
    }

    private void ChangeAlpha2()
    {
        Color tmp = GetComponent<SpriteRenderer>().color;
        tmp.a = 0f;
        GetComponent<SpriteRenderer>().color = tmp;
    }
}
