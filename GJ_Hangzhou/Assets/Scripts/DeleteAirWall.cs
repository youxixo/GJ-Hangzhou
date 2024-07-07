using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAirWall : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {

        // Find the game object with the tag "书"
        GameObject bookObject = GameObject.FindWithTag("书");

        // If no game object with the tag "书" is found, delete this game object
        if (bookObject == null)
        {
            Debug.Log("No game object with the tag '书' found. Deleting this game object.");
            Destroy(gameObject);
        }
    }
}
