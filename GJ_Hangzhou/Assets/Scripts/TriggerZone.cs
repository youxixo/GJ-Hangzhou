using System.Collections;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    public Collider2D doorCollider;
    public Collider2D cabinetCollider;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(DisableCollidersAfterDelay(2f));  // 2秒的延迟
        }
    }

    IEnumerator DisableCollidersAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        doorCollider.enabled = false;
        cabinetCollider.enabled = false;
    }
}