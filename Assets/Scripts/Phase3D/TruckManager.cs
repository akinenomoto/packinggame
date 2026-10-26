using System.Collections.Generic;
using UnityEngine;

public class TruckManager : MonoBehaviour
{
    public static TruckManager Instance { get; private set; }

    public Grid3DManager grid;

    // [SerializeField] private List<Item3DController> items = new();
    public List<Item3DController> items = new();  // ← publicに変更

    Item3DController _selected;
    int _placedCount = 0;

    void Awake()
    {
        Instance = this;
        if (grid == null) grid = GetComponent<Grid3DManager>();
    }

    void Start()
   {
    // Inspectorが空なら自動でシーン内の全Item3DControllerを取得
    if (items.Count == 0)
    {
        items.AddRange(FindObjectsOfType<Item3DController>());
        Debug.Log($"[TruckManager] アイテム自動検出: {items.Count}件");
    }
    Debug.Log($"[TruckManager] 管理アイテム数: {items.Count}");
    }

    public void SelectItem(Item3DController item)
    {
        _selected?.SetHighlight(false);
        _selected = item;
        _selected?.SetHighlight(true);
    }

    public Item3DController GetSelected() => _selected;

    public void OnItemPlaced(Item3DController item)
    {
        _selected?.SetHighlight(false);
        _selected = null;
        _placedCount++;
        Debug.Log($"[TruckManager] 配置済み {_placedCount} / {items.Count}");
        if (_placedCount >= items.Count && items.Count > 0)
            GameManager.Instance?.OnTruckLoadingComplete();
    }
}
