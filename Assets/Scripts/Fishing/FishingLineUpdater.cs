using UnityEngine;

public class FishingLineUpdater : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform[] points;

    private void Start()
    {
        if(lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }
        if (lineRenderer == null)
        {
            Debug.LogError("LineRenderer not found");
            enabled = false;
            return;
        }

        lineRenderer.positionCount = points.Length;
    }

    private void Update()
    {
        for (int i = 0; i < points.Length; i++)
        {
            if (points[i] != null)
            {
                lineRenderer.SetPosition(i, points[i].position);
            }
        }
    }
}
