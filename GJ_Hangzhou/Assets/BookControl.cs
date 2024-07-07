using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookControl : MonoBehaviour
{
    public GameObject player;  // Reference to the player game object

    private Collider triggerCollider;

    void Start()
    {
        // Get the collider component attached to the game object
        triggerCollider = GetComponent<Collider>();

        // Check if the collider is a trigger
        if (!triggerCollider.isTrigger)
        {
            Debug.LogError("The collider is not set as a trigger.");
        }
    }

    void Update()
    {
        // Check if the player's y-axis position is greater than the object's y-axis position
        if (player.transform.position.y > transform.position.y)
        {
            // Disable the trigger
            triggerCollider.enabled = false;
        }
        else
        {
            // Enable the trigger (if needed)
            triggerCollider.enabled = true;
        }
    }
}
