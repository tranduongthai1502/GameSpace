using UnityEngine;
using UnityEngine.SceneManagement; // Thêm thư viện quản lý Scene

public class MainMenuController : MonoBehaviour
{
    public GameObject soundCheckmark; // Kéo icon dấu check (✓) của nút Sound vào đây
    public GameObject controlsPanel;  // Kéo bảng ControlsPanel vào đây
    private bool soundState = true;

    // Hàm này sẽ được gọi khi bấm nút Play
    public void OnPlayButtonClicked()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayClickSound();
        }
        
        Debug.Log("Chuyển sang màn chơi Gameplay");
        
        // Chuyển sang màn chơi GameplayScene (đã thêm vào Build Profiles)
        SceneManager.LoadScene("GameplayScene");
    }

    public void OnSoundButtonClicked()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayClickSound();
            soundState = AudioManager.Instance.ToggleSound();
        }

        if (soundCheckmark != null)
        {
            soundCheckmark.SetActive(soundState);
        }
    }

    // Khi bấm nút Controls -> Hiện bảng hướng dẫn lên
    public void OnControlsButtonClicked()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayClickSound();
        }

        if (controlsPanel != null)
        {
            controlsPanel.SetActive(true);
        }
    }

    // Khi bấm nút X -> Đóng bảng hướng dẫn lại
    public void OnCloseControlsClicked()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayClickSound();
        }

        if (controlsPanel != null)
        {
            controlsPanel.SetActive(false);
        }
    }
}