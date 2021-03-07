using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Line : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;
    public float length = 0f;

    public List<Vector2> points;

    public void UpdateLine (Vector2 mousePos, float zAdjustment)
    {
        if (points == null)
        {
            points = new List<Vector2>();
        }
            SetPoint(mousePos, zAdjustment);
    }

    void SetPoint (Vector2 point, float zAdjustment)
    {
        points.Add(point);

        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPosition(points.Count - 1, new Vector3(point.x, point.y, zAdjustment));
        
        if (points.Count > 1)
            edgeCollider.points = points.ToArray();
    }
}
