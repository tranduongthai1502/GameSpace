using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenuUI; // Kéo PausePanelContainer vào đây
    private bool isPaused = false;

    [Header("UI Controls")]
    public Button pauseButton;
    public Button resumeButton;
    public Button reloadButton;
    public Button quitButton;
    public Slider musicSlider;
    public Slider bulletSlider;

    [Header("Audio References")]
    public AudioSource backgroundMusicSource; // Nguồn nhạc nền game
    // Âm thanh đạn sẽ lấy từ PlayerController hoặc AudioSource riêng

    void Start()
    {
        pauseMenuUI.SetActive(false);

        // Lắng nghe sự kiện click nút
        if (pauseButton != null) pauseButton.onClick.AddListener(TogglePause);
        if (resumeButton != null) resumeButton.onClick.AddListener(Resume);
        if (reloadButton != null) reloadButton.onClick.AddListener(ReloadGame);
        if (quitButton != null) quitButton.onClick.AddListener(QuitToMainMenu);

        // Thiết lập giá trị ban đầu cho thanh trượt âm lượng
        if (musicSlider != null)
        {
            musicSlider.value = 1f;
            musicSlider.onValueChanged.AddListener(SetMusicVolume);
        }
        if (bulletSlider != null)
        {
            bulletSlider.value = 1f;
            bulletSlider.onValueChanged.AddListener(SetBulletVolume);
        }
    }

    void Update()
    {
        // Bấm phím Esc để bật/tắt bảng tạm dừng
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Cho game chạy bình thường
        isPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Đóng băng toàn bộ hoạt động trong game
        isPaused = true;
    }

    public void TogglePause()
    {
        if (isPaused) Resume();
        else Pause();
    }

    public void ReloadGame()
    {
        Time.timeScale = 1f; // Phục hồi thời gian trước khi load lại scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenuScene"); // Thay tên Scene Main Menu của bạn vào đây nếu khác tên
    }

    public void SetMusicVolume(float volume)
    {
        if (backgroundMusicSource != null)
        {
            backgroundMusicSource.volume = volume;
        }
    }

    public void SetBulletVolume(float volume)
    {
        // Điều chỉnh âm lượng cho Player hoặc các âm thanh đạn
        AudioListener.volume = volume; // Hoặc tinh chỉnh riêng cho AudioSource đạn của Player
    }
}