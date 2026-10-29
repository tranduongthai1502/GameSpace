using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 12f; // Tốc độ bay của đạn

    void Update()
    {
        // Dùng Vector3.up nhân trực tiếp với Vector thế giới, độc lập hoàn toàn với máy bay
        transform.position += Vector3.up * speed * Time.deltaTime;

        // Tự động hủy đạn khi bay lên cao quá giới hạn màn hình (Y > 4.5f)
        if (transform.position.y > 6.5f)
        {
            Destroy(gameObject);
        }
    }
}