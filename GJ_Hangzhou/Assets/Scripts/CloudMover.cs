using System.Collections;
using UnityEngine;

public class CloudMover : MonoBehaviour
{
    public Transform fan;  // 风扇的Transform
    public float moveSpeed = 1f;  // 云朵移动的速度
    public float shrinkSpeed = 0.5f;  // 云朵缩小的速度
    public Vector3 initialScale;  // 云朵的初始大小
    public Vector3 finalScale = Vector3.zero;  // 云朵的最终大小
    public Vector3 floatAmplitude = new Vector3(0, 0.1f, 0);  // 云朵的浮动幅度
    public float floatSpeed = 1f;  // 云朵的浮动速度

    private bool shouldMove = false;
    private Vector3 initialPosition;

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
            FloatEffect();
        }
    }

    public void StartMoving()
    {
        Debug.Log("CloudMover: StartMoving called");
        shouldMove = true;
    }

    private void MoveAndShrink()
    {
        // 移动到风扇位置
        transform.position = Vector3.MoveTowards(transform.position, fan.position, moveSpeed * Time.deltaTime);
        Debug.Log("CloudMover: Moving towards fan");

        // 缩小
        transform.localScale = Vector3.Lerp(transform.localScale, finalScale, shrinkSpeed * Time.deltaTime);
        Debug.Log("CloudMover: Shrinking");

        // 到达风扇后停止移动
        if (transform.position == fan.position)
        {
            Debug.Log("CloudMover: Reached fan position");
            shouldMove = false;
        }
    }

    private void FloatEffect()
    {
        // 模拟浮动效果
        transform.position = initialPosition + floatAmplitude * Mathf.Sin(Time.time * floatSpeed);
        Debug.Log("CloudMover: Floating");
    }
}