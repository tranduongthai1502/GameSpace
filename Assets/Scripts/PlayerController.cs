using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private bool canControl = false;
    private bool hasClickedToStart = false;

    private Vector3 startPos = new Vector3(0f, -6f, 0f);
    private Vector3 middlePos = new Vector3(0f, 0f, 0f);
    private Vector3 playPos = new Vector3(0f, -3f, 0f);

    [Header("Cấu hình bắn đạn")]
    public GameObject bulletPrefab;
    public Transform firePointLeft;
    public Transform firePointRight;
    public float fireRate = 0.15f;
    private float fireTimer = 0f;

    [Header("Cấu hình âm thanh")]
    public AudioSource audioSource; // Kéo Audio Source vào đây

    void Start()
    {
        transform.position = startPos;
        canControl = false;
        hasClickedToStart = false;

        // Tự động lấy AudioSource nếu quên kéo
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        StartCoroutine(IntroRoutine());
    }

    IEnumerator IntroRoutine()
    {
        float duration = 1.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            transform.position = Vector3.Lerp(startPos, middlePos, t);
            yield return null;
        }

        elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            transform.position = Vector3.Lerp(middlePos, playPos, t);
            yield return null;
        }

        yield return new WaitForSeconds(2.0f);
        canControl = true;
    }

    void Update()
    {
        if (!canControl) return;

        if (!hasClickedToStart)
        {
            if (Input.GetMouseButtonDown(0))
            {
                hasClickedToStart = true;
            }
            return;
        }

        // Di chuyển theo chuột
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 targetPosition = new Vector3(mousePosition.x, mousePosition.y, 0f);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 15f);

        // Giữ chuột trái để bắn đạn
        if (Input.GetMouseButton(0))
        {
            fireTimer += Time.deltaTime;
            if (fireTimer >= fireRate)
            {
                fireTimer = 0f;
                Shoot();
            }
        }
        else
        {
            fireTimer = fireRate;
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null)
        {
            if (firePointLeft != null)
            {
                Instantiate(bulletPrefab, firePointLeft.position, Quaternion.identity);
            }
            if (firePointRight != null)
            {
                Instantiate(bulletPrefab, firePointRight.position, Quaternion.identity);
            }

            // Phát hiệu ứng âm thanh bắn đạn
            if (audioSource != null && audioSource.clip != null)
            {
                audioSource.PlayOneShot(audioSource.clip);
            }
        }
    }
}