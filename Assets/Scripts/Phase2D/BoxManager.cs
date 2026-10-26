using UnityEngine;

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

    void Awake()
    {
        Grid = GetComponent<Grid2DManager>();
    }

    /// <summary>外部（ボタンなど）から蓋を閉める</summary>
    public void RequestClose()
    {
        if (_state != BoxState.Open) return;
        _state = BoxState.Closed;

        if (lidController != null)
            lidController.PlayCloseAnimation();
        else
            OnCloseAnimationComplete();
    }

    /// <summary>LidController のアニメーション完了後に呼ばれる</summary>
    public void OnCloseAnimationComplete()
    {
        Debug.Log($"[BoxManager] Box {BoxID} 梱包完了");
        TransferManager.Instance?.RegisterClosedBox(this);
        BoxSetManager.Instance?.OnBoxClosed(this);
    }
}