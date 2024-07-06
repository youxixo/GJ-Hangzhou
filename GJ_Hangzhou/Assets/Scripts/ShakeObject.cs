using System.Collections;
using UnityEngine;

public class ShakeObject : MonoBehaviour
{
    public float shakeDuration = 0.5f;  // Duration of the shake
    public float shakeMagnitude = 0.1f; // Magnitude of the shake

    private Vector3 initialPosition;
    private bool isShaking = false;
    private bool hasItemDropped = false;

    void Start()
    {
        initialPosition = transform.localPosition;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isShaking && !hasItemDropped)
        {
            StartCoroutine(Shake());
        }
    }

    IEnumerator Shake()
    {
        isShaking = true;
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            elapsedTime += Time.deltaTime;
            float x = initialPosition.x;
            float y = initialPosition.y + Random.Range(-shakeMagnitude, shakeMagnitude);
            transform.localPosition = new Vector3(x, y, initialPosition.z);

            // Apply a downward force to objects on top of the cabinet
            ApplyDownwardForce();

            yield return null;
        }

        transform.localPosition = initialPosition;
        isShaking = false;

        // Check if the item has dropped
        CheckIfItemDropped();
    }

    void ApplyDownwardForce()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, transform.localScale, 0f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != gameObject)
            {
                Rigidbody2D rb = collider.attachedRigidbody;
                if (rb != null && !rb.isKinematic)
                {
                    rb.AddForce(Vector2.down * 5f, ForceMode2D.Impulse); // Adjust the force value as needed
                }
            }
        }
    }

    void CheckIfItemDropped()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, transform.localScale, 0f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != gameObject)
            {
                // If the item is still on the cabinet, return
                return;
            }
        }

        // If no items are found on top of the cabinet, set hasItemDropped to true
        hasItemDropped = true;
    }
}
