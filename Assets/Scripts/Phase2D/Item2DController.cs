using System.Collections.Generic;
using UnityEngine;

public class Item2DController : MonoBehaviour
{
    [Header("形状（セル座標オフセット）")]
    public Vector2Int[] baseShape = { new(0, 0) };

    // 内部状態
    int _rotIndex = 0;
    Vector3 _originalPos;
    bool _isDragging = false;

    BoxManager _placedBox;
    List<Vector2Int> _placedCells = new();

    Placement2D _placement;
    SpriteRenderer _sr;

    void Start()
    {
        _placement   = FindObjectOfType<Placement2D>();
        _sr          = GetComponent<SpriteRenderer>();
        _originalPos = transform.position;
    }

    void OnMouseDown()
    {
        // 配置済みなら解除
        if (_placedBox != null)
        {
            _placedBox.Grid.Remove(_placedCells);
            _placedCells.Clear();
            _placedBox = null;
        }

        _originalPos = transform.position;
        _isDragging  = true;
        _sr.sortingOrder = 10;
    }

    void OnMouseDrag()
    {
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0;
        transform.position = mouseWorld;

        _placement?.UpdatePreview(this);
    }

    void OnMouseUp()
    {
        _isDragging = false;
        _placement?.HidePreview();

        var box = _placement?.GetBoxUnderMouse();
        if (box == null || box.IsLocked)
        {
            ReturnToOrigin();
            return;
        }

        var grid  = box.Grid;
        var cells = GetWorldCells(grid);

        if (grid.CanPlace(cells, this))
        {
            // スナップ位置に移動
            transform.position = GetSnapPosition(grid, cells);
            transform.rotation = Quaternion.Euler(0, 0, -_rotIndex * 90f);

            grid.Place(cells, this);
            _placedBox   = box;
            _placedCells = new List<Vector2Int>(cells);

            _sr.sortingOrder = 5;
            RoomManager.Instance?.OnItemPacked(this);
        }
        else
        {
            ReturnToOrigin();
        }
    }

    void Update()
    {
        if (!_isDragging) return;

        if (Input.GetKeyDown(KeyCode.R))
        {
            _rotIndex = (_rotIndex + 1) % 4;
            transform.rotation = Quaternion.Euler(0, 0, -_rotIndex * 90f);
            _placement?.UpdatePreview(this);
        }
    }

    // ─── ヘルパー ────────────────────────────

    void ReturnToOrigin()
    {
        transform.position = _originalPos;
        transform.rotation = Quaternion.identity;
        _rotIndex        = 0;
        _sr.sortingOrder = 5;
    }

    /// <summary>回転後のセル座標リストを返す</summary>
    public List<Vector2Int> GetRotatedShape()
    {
        var result = new List<Vector2Int>();
        foreach (var c in baseShape)
            result.Add(RotateCell(c, _rotIndex));
        return result;
    }

    /// <summary>グリッド上のセル座標（アイテム現在位置基準）</summary>
    public List<Vector2Int> GetWorldCells(Grid2DManager grid)
    {
        Vector2Int origin = grid.WorldToCell(transform.position);
        var cells = new List<Vector2Int>();
        foreach (var offset in GetRotatedShape())
            cells.Add(origin + offset);
        return cells;
    }

    Vector3 GetSnapPosition(Grid2DManager grid, List<Vector2Int> cells)
    {
        Vector2 sum = Vector2.zero;
        foreach (var c in cells) sum += grid.CellToWorld(c);
        Vector2 center = sum / cells.Count;
        return new Vector3(center.x, center.y, 0);
    }

    static Vector2Int RotateCell(Vector2Int cell, int times)
    {
        for (int i = 0; i < times; i++)
            cell = new Vector2Int(-cell.y, cell.x);
        return cell;
    }
}