using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform Destination;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerMovement>() == null)
            return;

        Transform transform = other.GetComponent<Transform>();
        transform.position = new Vector3(Destination.transform.position.x, Destination.transform.position.y, transform.position.z);
    }
}
