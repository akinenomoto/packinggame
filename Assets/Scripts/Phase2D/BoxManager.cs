using UnityEngine;
using System.Collections.Generic;

public class BoxManager : MonoBehaviour
{
    [Header("設定")]
    public int BoxID;

    [Header("参照")]
    public LidController lidController;

    public Grid2DManager Grid { get; private set; }

    public enum BoxState { Open, Closed }
    BoxState _state = BoxState.Open;

    public bool IsLocked => _state != BoxState.Open;

    // この箱に収納されたアイテムを記録
    List<Item2DController> _packedItems = new();

    void Awake()
    {
        Grid = GetComponent<Grid2DManager>();
    }

    /// <summary>アイテムが収納されたときに登録</summary>
    public void RegisterItem(Item2DController item)
    {
        if (!_packedItems.Contains(item))
            _packedItems.Add(item);
    }

    public void RequestClose()
    {
        if (_state != BoxState.Open) return;
        _state = BoxState.Closed;

        if (lidController != null)
            lidController.PlayCloseAnimation();
        else
            OnCloseAnimationComplete();
    }

    public void OnCloseAnimationComplete()
    {
        // 収納済みアイテムを全て非表示
        foreach (var item in _packedItems)
        {
            if (item != null)
                item.gameObject.SetActive(false);
        }

        Debug.Log($"[BoxManager] Box {BoxID} 梱包完了");
        TransferManager.Instance?.RegisterClosedBox(this);
        BoxSetManager.Instance?.OnBoxClosed(this);
    }
}