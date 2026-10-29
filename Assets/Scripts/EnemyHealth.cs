// Quản lý máu, sát thương và trạng thái chết của quái/Boss.
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Máu của quái")]
    [Min(1)]
    public int maxHealth = 2;
    public bool isBoss = false;
    private int currentHealth;

    [Header("Hiệu ứng bị hủy")]
    public GameObject explosionEffectPrefab;
    public GameObject starPrefab;
    public AudioClip hitSound;
    public AudioClip explosionSound;

    private bool isDead;

    // Đặt máu hiện tại, luôn đảm bảo có ít nhất một máu.
    private void Awake()
    {
        currentHealth = Mathf.Max(1, maxHealth);
    }

    // Trừ một máu và gọi Die khi máu giảm về 0.
    public void TakeDamage()
    {
        if (isDead) return;

        currentHealth--;
        if (currentHealth > 0) return;

        Die();
    }

    // Thông báo quái chết, phát hiệu ứng, thả sao và hủy object.
    private void Die()
    {
        isDead = true;

        if (GameManager.Instance != null && isBoss)
        {
            GameManager.Instance.NotifyBossKilled();
        }
        else if (GameManager.Instance != null)
        {
            GameManager.Instance.NotifyEnemyKilled();
        }

        AudioClip deathSound = explosionSound != null ? explosionSound : hitSound;
        if (deathSound != null)
        {
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
        }

        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        }

        // Mỗi quái bị tiêu diệt sẽ thả một ngôi sao tại vị trí của nó.
        if (starPrefab != null)
        {
            Instantiate(starPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
