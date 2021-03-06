using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public Transform topRightCorner;
    public Transform bottomLeftCorner;

    public bool IsInBox(Vector2 pos)
    {
        return pos.x < topRightCorner.position.x && pos.x > bottomLeftCorner.position.x &&
               pos.y < topRightCorner.position.y && pos.y > bottomLeftCorner.position.y;
    }

    void OnDrawGizmos()
    {
        if (topRightCorner != null && bottomLeftCorner != null)
        {
            Vector3 topLeftCorner = new Vector3(bottomLeftCorner.position.x, topRightCorner.position.y, topRightCorner.position.z);
            Vector3 bottomRightCorner = new Vector3(topRightCorner.position.x, bottomLeftCorner.position.y, topRightCorner.position.z);
            Gizmos.DrawLine(topLeftCorner, topRightCorner.position);
            Gizmos.DrawLine(topRightCorner.position, bottomRightCorner);
            Gizmos.DrawLine(bottomRightCorner, bottomLeftCorner.position);
            Gizmos.DrawLine(bottomLeftCorner.position, topLeftCorner);
        }
    }
}
