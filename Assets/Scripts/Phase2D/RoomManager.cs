using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance { get; private set; }

    [Header("部屋内の全アイテム")]
    [SerializeField] private List<Item2DController> items = new();

    int _packedCount = 0;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void OnItemPacked(Item2DController item)
    {
        _packedCount++;
        Debug.Log($"[RoomManager] 収納: {_packedCount} / {items.Count}");

        if (_packedCount >= items.Count)
            Debug.Log("[RoomManager] 全アイテム収納完了！　蓋を閉めてください");
    }
}
