using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public ChangeLevelTrigger Goal;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerMovement>() == null)
            return;

        Goal.Items++;

        Destroy(this.gameObject);
    }

}
