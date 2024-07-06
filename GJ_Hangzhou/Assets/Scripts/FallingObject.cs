using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    public float fallDuration = 2f;  // Time taken to fall
    public float disappearDelay = 1f;  // Time before the object disappears

    private bool isFalling = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!isFalling)
            {
                StartCoroutine(FallAndDisappear());
            }
        }
    }

    IEnumerator FallAndDisappear()
    {
        isFalling = true;
        float elapsedTime = 0f;
        Vector3 initialRotation = transform.rotation.eulerAngles;

        while (elapsedTime < fallDuration)
        {
            elapsedTime += Time.deltaTime;
            float angle = Mathf.Lerp(0, 90, elapsedTime / fallDuration);  // Rotate 90 degrees
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, initialRotation.z + angle));
            yield return null;
        }

        yield return new WaitForSeconds(disappearDelay);
        Destroy(gameObject);  // Destroy the object
    }
}