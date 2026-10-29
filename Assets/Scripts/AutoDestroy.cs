// Tự động hủy các object hiệu ứng sau một khoảng thời gian.
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    // Bắt đầu bộ đếm thời gian hủy object.
    void Start()
    {
        Destroy(gameObject, 0.8f); // Tự hủy sau 0.5 giây
    }
}
