// Quản lý nhạc nền và hiệu ứng âm thanh dùng chung giữa các scene.
using UnityEngine;

// Singleton điều khiển trạng thái âm thanh của toàn bộ game.
public class AudioManager : MonoBehaviour
{
    // Instance dùng để các script khác truy cập AudioManager.
    public static AudioManager Instance;

    public AudioSource musicSource;   // Audio Source thứ nhất (Nhạc nền)
    public AudioSource sfxSource;     // Audio Source thứ hai (Tiếng click)

    // Trạng thái bật/tắt âm thanh hiện tại.
    private bool isSoundOn = true;

    // Khởi tạo Singleton và giữ object khi chuyển scene.
    private void Awake()
    {
        // Đảm bảo AudioManager không bị mất khi chuyển scene
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // Hàm bật/tắt toàn bộ âm thanh khi bấm nút Sound
    // Đảo trạng thái âm thanh và trả về trạng thái mới.
    public bool ToggleSound()
    {
        isSoundOn = !isSoundOn;
        AudioListener.volume = isSoundOn ? 1f : 0f;
        return isSoundOn;
    }

    // Hàm phát tiếng click khi bấm nút
    // Phát âm thanh click nếu âm thanh đang bật và AudioSource không bận.
    public void PlayClickSound()
    {
        if (isSoundOn && sfxSource != null && sfxSource.isPlaying == false)
        {
            sfxSource.Play();
        }
    }
}
