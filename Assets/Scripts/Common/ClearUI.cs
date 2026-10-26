using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearUI : MonoBehaviour
{
    [Header("UI")]
    public GameObject clearPanel;

    public static ClearUI Instance { get; private set; }

    void Awake()
    {
        Instance = this;
        if (clearPanel != null)
            clearPanel.SetActive(false);
    }

    public void ShowClear()
    {
        if (clearPanel != null)
            clearPanel.SetActive(true);
    }

    public void OnRestartButton()
    {
        SceneManager.LoadScene(0); // RoomScene に戻る
    }
}