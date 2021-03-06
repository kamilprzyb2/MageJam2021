using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDrawings : MonoBehaviour
{
    public float drawingIntervals = 0.5f;
    public float startDelay = 1f;
    private List<LineRenderer> lineRenderers;
    void Start()
    {
        lineRenderers = new List<LineRenderer>();
        foreach (LineRenderer renderer in GetComponentsInChildren<LineRenderer>())
        {
            renderer.enabled = false;
        }
        StartCoroutine(Draw());
    }

    IEnumerator Draw()
    {
        yield return new WaitForSeconds(startDelay);
        foreach (LineRenderer renderer in GetComponentsInChildren<LineRenderer>())
        {
            renderer.enabled = true;
            yield return new WaitForSeconds(drawingIntervals);
        }
    }
}
