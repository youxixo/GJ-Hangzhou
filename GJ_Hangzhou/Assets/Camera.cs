using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform player;        // 玩家对象的Transform
    public Vector3 offset;          // 摄像机与玩家的偏移量
    public float smoothSpeed = 0.125f; // 摄像机平滑移动的速度

    // 摄像机移动的边界
    public float minX = 2.3f;
    public float maxX = 206.4f;
    public float minY = 1.4f;
    public float maxY = 23f;

    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 desiredPosition = player.position + offset;
            desiredPosition.z = transform.position.z; // 确保摄像机的Z轴位置不变

            // 平滑移动摄像机
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // 限制摄像机的X和Y位置
            smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, minX, maxX);
            smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, minY, maxY);

            // 更新摄像机的位置
            transform.position = smoothedPosition;
        }
    }
}


