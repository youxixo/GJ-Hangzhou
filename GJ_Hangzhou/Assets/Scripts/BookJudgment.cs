using UnityEngine;

public class BookJudgment : MonoBehaviour
{
    public Collider2D bed; // 床的碰撞体
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 获取书的Rigidbody2D组件
        Rigidbody2D bookRigidbody = GetComponent<Rigidbody2D>();
        if (bookRigidbody != null)
        {
            // 删除书的Rigidbody2D组件
            Destroy(bookRigidbody);
        }
        // 检查碰撞到的物体是否是床
        if (collision.collider == bed)
        {
            // 获取书的碰撞体
            Collider2D bookCollider = GetComponent<Collider2D>();
            if (bookCollider != null)
            {
                // 将书的碰撞体变成触发器
                bookCollider.isTrigger = true;
            }

        }
    }
}