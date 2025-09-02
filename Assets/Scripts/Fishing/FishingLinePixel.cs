using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class FishingLinePixel : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public GameObject pixelPrefab;
    private float zDepth = -5f;
    [SerializeField] private float pixelSize;

    List<GameObject> pool = new List<GameObject>();

    private void Update()
    {
        if (pointA == null || pointB == null || pixelPrefab == null) return;
        DrawLine();
    }

    private void DrawLine()
    {
        // converts world position to pixel position
        Vector2 worldA = new Vector2(pointA.position.x, pointA.position.y);
        Vector2 worldB = new Vector2(pointB.position.x, pointB.position.y);

        Vector2Int start = Vector2Int.RoundToInt(worldA / pixelSize);
        Vector2Int end = Vector2Int.RoundToInt(worldB / pixelSize);

        // get pixel pos with bresenham in pixel space
        List<Vector2Int> pixelsOnLine = BresenhamLine(start, end);

        // ensure pool size
        EnsurePoolSize(pixelsOnLine.Count);

        // compute how to scale pixel prefab
        SpriteRenderer prefabSR = pixelPrefab.GetComponent<SpriteRenderer>();
        float spriteWorldSize = 1f;
        if (prefabSR != null && prefabSR != null)
        {
            spriteWorldSize = prefabSR.sprite.bounds.size.x;
        }

        float desiredScale = pixelSize / spriteWorldSize;

        // place active pixels
        for (int i = 0; i < pixelsOnLine.Count; i++)
        {
            Vector2Int p = pixelsOnLine[i];
            Vector3 worldPos = new Vector3(p.x * pixelSize, p.y * pixelSize, zDepth);

            GameObject g = pool[i];
            g.transform.position = worldPos;
            g.transform.localScale = Vector3.one * desiredScale;
            if(!g.activeSelf) g.SetActive(true);
        }

        // disable unused pixels
        for (int i = pixelsOnLine.Count; i < pool.Count; i++)
        {
            pool[i].SetActive(false);
        }
    }

    private void EnsurePoolSize(int needed)
    {
        while (pool.Count < needed)
        {
            GameObject g = Instantiate(pixelPrefab, transform);
            g.SetActive(false);
            pool.Add(g);
        }
    }

    List<Vector2Int> BresenhamLine(Vector2Int p0, Vector2Int p1)
    {
        List<Vector2Int> list = new List<Vector2Int>();
        int x0 = p0.x, y0 = p0.y;
        int x1 = p1.x, y1 = p1.y;

        int dx = Mathf.Abs(x1 - x0), sx = x0 < x1 ? 1 : -1;
        int dy = -Mathf.Abs(y1 - y0), sy = y0 < y1 ? 1 : -1;
        int err = dx + dy;

        while (true)
        {
            list.Add(new Vector2Int(x0, y0));
            if (x0 == x1 && y0 == y1) break;
            int e2 = 2 * err;
            if (e2 >= dy)
            {
                err += dy;
                x0 += sx;
            }
            if (e2 <= dx)
            {
                err += dx;
                y0 += sy;
            }
            
        }
        return list;
    }
}
