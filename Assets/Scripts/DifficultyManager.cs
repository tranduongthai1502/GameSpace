// Lưu và quản lý mức độ khó người chơi chọn.
using UnityEngine;
using TMPro;

public class DifficultyManager : MonoBehaviour
{
    // Instance được giữ lại để truy cập lựa chọn độ khó từ các scene khác.
    public static DifficultyManager Instance;

    public int selectedDifficulty = 0; // 0: Easy, 1: Normal, 2: Hard
    public TMP_Dropdown difficultyDropdown; 

    // Khởi tạo Singleton và không hủy object khi chuyển scene.
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Đồng bộ Dropdown và đăng ký sự kiện thay đổi độ khó.
    void Start()
    {
        // Tự động kết nối sự kiện khi Dropdown thay đổi giá trị mà không cần gán thủ công ở Inspector
        if (difficultyDropdown != null)
        {
            // Đồng bộ giá trị hiện tại
            selectedDifficulty = difficultyDropdown.value;

            // Lắng nghe sự kiện người chơi đổi độ khó
            difficultyDropdown.onValueChanged.AddListener(OnDifficultyChanged);
        }
    }

    // Cập nhật độ khó khi người chơi chọn một giá trị mới.
    public void OnDifficultyChanged(int index)
    {
        selectedDifficulty = index;
        Debug.Log("Độ khó đã chọn: " + selectedDifficulty);
    }
}
