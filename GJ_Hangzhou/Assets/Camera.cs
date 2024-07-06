using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform player;        // 玩家对象的Transform
    public Vector3 offset;          // 摄像机与玩家的偏移量
    public float smoothSpeed = 0.125f; // 摄像机平滑移动的速度

    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 desiredPosition = player.position + offset;
            desiredPosition.z = transform.position.z; // 确保摄像机的Z轴位置不变
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}

