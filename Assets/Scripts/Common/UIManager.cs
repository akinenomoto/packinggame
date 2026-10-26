using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("UI参照")]
    public TextMeshProUGUI statusText;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void ShowRoomUI()
    {
        SetStatus("荷物を段ボールに収納してください");
    }

    public void ShowTruckUI()
    {
        SetStatus("全ての箱を閉めました！　トラックへ積み込みます...");
    }

    public void ShowClearScreen()
    {
        SetStatus("クリア！　おめでとうございます！");
    }

    public void ShowFailedScreen()
    {
        SetStatus("時間切れ…　もう一度挑戦しましょう");
    }

    public void UpdateItemCount(int remaining)
    {
        // 今回は StatusText だけ使うので省略
    }

    void SetStatus(string msg)
    {
        if (statusText != null) statusText.text = msg;
        Debug.Log($"[UIManager] {msg}");
    }
}