using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance { get; private set; }

    [Header("部屋内の全アイテム")]
    [SerializeField] private List<Item2DController> items = new();

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI packedCountText;

    int _packedCount = 0;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        UpdateCountText();
    }

    public void OnItemPacked(Item2DController item)
    {
        _packedCount++;
        UpdateCountText();
        Debug.Log($"[RoomManager] 収納: {_packedCount} / {items.Count}");

        if (_packedCount >= items.Count)
            Debug.Log("[RoomManager] 全アイテム収納完了！　蓋を閉めてください");
    }

    void UpdateCountText()
    {
        if (packedCountText != null)
            packedCountText.text = $"{_packedCount} / {items.Count}";
    }
}