using System.Collections.Generic;
using UnityEngine;

public class BoxSetManager : MonoBehaviour
{
    public static BoxSetManager Instance { get; private set; }

    [Header("全段ボール")]
    [SerializeField] private List<BoxManager> boxes = new();

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void OnBoxClosed(BoxManager box)
    {
        bool allClosed = boxes.TrueForAll(b => b.IsLocked);
        if (allClosed)
        {
            Debug.Log("[BoxSetManager] 全箱梱包完了 → トラックフェーズへ");
            GameManager.Instance?.OnAllBoxesClosed();
        }
    }
}