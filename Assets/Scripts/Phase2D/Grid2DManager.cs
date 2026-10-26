using System.Collections.Generic;
using UnityEngine;

public class Grid2DManager : MonoBehaviour
{
    [Header("グリッド設定")]
    public int columns = 4;
    public int rows    = 4;
    public float cellSize = 0.5f;
    public Vector2 gridOrigin;   // 左下隅のワールド座標

    bool[,]              _occupied;
    Item2DController[,]  _items;

    void Awake()
    {
        _occupied = new bool[columns, rows];
        _items    = new Item2DController[columns, rows];
    }

    // ─── 判定 ────────────────────────────────

    public bool InBounds(Vector2Int cell) =>
        cell.x >= 0 && cell.x < columns &&
        cell.y >= 0 && cell.y < rows;

    /// <summary>セルに配置可能か（ignore は自分自身を除外するため）</summary>
    public bool CanPlace(IEnumerable<Vector2Int> cells, Item2DController ignore = null)
    {
        foreach (var c in cells)
        {
            if (!InBounds(c)) return false;
            if (_occupied[c.x, c.y] && _items[c.x, c.y] != ignore) return false;
        }
        return true;
    }

    // ─── 操作 ────────────────────────────────

    public void Place(IEnumerable<Vector2Int> cells, Item2DController item)
    {
        foreach (var c in cells)
        {
            _occupied[c.x, c.y] = true;
            _items[c.x, c.y]    = item;
        }
    }

    public void Remove(IEnumerable<Vector2Int> cells)
    {
        foreach (var c in cells)
        {
            _occupied[c.x, c.y] = false;
            _items[c.x, c.y]    = null;
        }
    }

    public bool IsFull()
    {
        for (int x = 0; x < columns; x++)
            for (int y = 0; y < rows; y++)
                if (!_occupied[x, y]) return false;
        return true;
    }

    // ─── 座標変換 ────────────────────────────

    public Vector2Int WorldToCell(Vector2 worldPos)
    {
        Vector2 local = worldPos - gridOrigin;
        return new Vector2Int(
            Mathf.FloorToInt(local.x / cellSize),
            Mathf.FloorToInt(local.y / cellSize)
        );
    }

    public Vector2 CellToWorld(Vector2Int cell)
    {
        return gridOrigin + new Vector2(
            (cell.x + 0.5f) * cellSize,
            (cell.y + 0.5f) * cellSize
        );
    }
}