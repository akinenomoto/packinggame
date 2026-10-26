using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum Phase { Packing, Truck, Clear, Failed }
    public Phase CurrentPhase { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        CurrentPhase = Phase.Packing;
        UIManager.Instance?.ShowRoomUI();
        Debug.Log("[GameManager] 梱包フェーズ開始");
    }

    public void OnAllBoxesClosed()
    {
        if (CurrentPhase != Phase.Packing) return;
        CurrentPhase = Phase.Truck;
        UIManager.Instance?.ShowTruckUI();
        Debug.Log("[GameManager] 全箱梱包完了 → 1.5秒後にトラックシーンへ");
        Invoke(nameof(LoadTruckScene), 1.5f);
    }

    void LoadTruckScene()
    {
        SceneManager.LoadScene(1);
    }

    public void OnTruckLoadingComplete()
   {
    CurrentPhase = Phase.Clear;
    UIManager.Instance?.ShowClearScreen();
    ClearUI.Instance?.ShowClear(); // ← 追加
   }

    public void OnTimeUp()
    {
        CurrentPhase = Phase.Failed;
        UIManager.Instance?.ShowFailedScreen();
    }
}
