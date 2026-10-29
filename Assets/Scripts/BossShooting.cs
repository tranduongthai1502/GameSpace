// Điều khiển cơ chế bắn đạn của Boss.
using UnityEngine;

public class BossShooting : MonoBehaviour
{
    // Prefab đạn được Boss tạo ra.
    public GameObject bossBulletPrefab;

    public float fireInterval = 2f;
    public float bulletSpeed = 4f;
    public float spreadAngle = 25f;

    // Thời gian đã trôi qua kể từ lần bắn gần nhất.
    private float fireTimer;

    // Cập nhật bộ đếm và bắn theo chu kỳ.
    void Update()
    {
        fireTimer += Time.deltaTime;

        if (fireTimer >= fireInterval)
        {
            ShootThreeBullets();
            fireTimer = 0f;
        }
    }

    // Tạo ba viên đạn theo ba hướng khác nhau.
    void ShootThreeBullets()
    {
        if (bossBulletPrefab == null) return;

        // Vị trí xuất phát ngay tại Boss, hơi lệch xuống
        Vector3 spawnPosition = transform.position + Vector3.down * 0.6f;

        // Bắn 3 viên: trái, giữa, phải
        for (int i = -1; i <= 1; i++)
        {
            Vector2 direction =
                Quaternion.Euler(0f, 0f, i * spreadAngle) * Vector2.down;

            GameObject bullet = Instantiate(
                bossBulletPrefab,
                spawnPosition,
                Quaternion.identity
            );

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.linearVelocity = direction * bulletSpeed;
            }

            Destroy(bullet, 6f);
        }
    }
}
