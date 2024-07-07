using System.Collections;
using UnityEngine;

public class CloudMoverment : MonoBehaviour
{
    public Transform fan;  // 风扇的Transform
    public float moveSpeed = 1f;  // 云朵移动的速度
    public float shrinkSpeed = 0.5f;  // 云朵缩小的速度
    public Vector3 finalScale = Vector3.zero;  // 云朵的最终大小
    public Vector3 floatAmplitude = new Vector3(0, 0.1f, 0);  // 云朵的浮动幅度
    public float floatSpeed = 1f;  // 云朵的浮动速度

    private bool shouldMove = false;
    private Vector3 initialPosition;
    private Vector3 initialScale;

    void Start()
    {
        initialPosition = transform.position;
        initialScale = transform.localScale;
    }

    void Update()
    {
        if (shouldMove)
        {
            MoveAndShrink();
        }
        else
        {
            FloatEffect();
        }
    }

    public void StartMoving()
    {
        Debug.Log("CloudMover: StartMoving called");
        shouldMove = true;
        initialPosition = transform.position; // 确保浮动效果基于最新位置
    }

    private void MoveAndShrink()
    {
        // 移动和缩小
        transform.position = Vector3.MoveTowards(transform.position, fan.position, moveSpeed * Time.deltaTime);
        transform.localScale = Vector3.Lerp(transform.localScale, finalScale, shrinkSpeed * Time.deltaTime);

        Debug.Log("CloudMover: Moving and shrinking, Position: " + transform.position + ", Scale: " + transform.localScale);

        // 当云朵接近目标位置并且足够小的时候
        if (Vector3.Distance(transform.position, fan.position) < 0.1f && transform.localScale.magnitude <= 0.01f)
        {
            Debug.Log("CloudMover: Reached fan position and shrunk enough, removing colliders and deactivating");
            RemoveColliders();
            shouldMove = false; // 停止移动和缩小

            Debug.Log("CloudMover: Cloud disappeared");
            Destroy(gameObject); // 删除云朵对象
        }
    }

    private void RemoveColliders()
    {
        Collider2D[] colliders2D = GetComponents<Collider2D>();
        Collider[] colliders = GetComponents<Collider>();

        foreach (var collider in colliders2D)
        {
            Destroy(collider);
        }

        foreach (var collider in colliders)
        {
            Destroy(collider);
        }

        Debug.Log("CloudMover: Removed all colliders");
    }

    private void FloatEffect()
    {
        // 模拟浮动效果
        transform.position = initialPosition + floatAmplitude * Mathf.Sin(Time.time * floatSpeed);
        Debug.Log("CloudMover: Floating, Position: " + transform.position);
    }
}
