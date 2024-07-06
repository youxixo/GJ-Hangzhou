using UnityEngine;

public class FanController : MonoBehaviour
{
    public float upwardForce = 10f;  // 向上的力
    private bool isFanActive = false;  // 风扇是否启动

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isFanActive && other.CompareTag("Player"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(Vector2.up * upwardForce, ForceMode2D.Impulse);
                Debug.Log("Player bounced up by the fan");
            }
        }
    }

    public void ActivateFan()
    {
        isFanActive = true;
        Debug.Log("Fan activated");
    }
}