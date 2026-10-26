using System.Collections.Generic;
using UnityEngine;

public class Grid3DManager : MonoBehaviour
{
    [Header("グリッド設定")]
    public int xSize = 4;
    public int ySize = 3;
    public int zSize = 4;
    public float cellSize = 1f;
    public Vector3 gridOrigin = new(-2, 0, -2);

    bool[,,] _occupied;

    void Awake()
    {
        _occupied = new bool[xSize, ySize, zSize];
    }

    public bool InBounds(Vector3Int cell) =>
        cell.x >= 0 && cell.x < xSize &&
        cell.y >= 0 && cell.y < ySize &&
        cell.z >= 0 && cell.z < zSize;

    public bool CanPlace(List<Vector3Int> cells)
    {
        foreach (var c in cells)
        {
            if (!InBounds(c) || _occupied[c.x, c.y, c.z])
                return false;
        }
        return true;
    }

    public void Place(List<Vector3Int> cells)
    {
        foreach (var c in cells)
            _occupied[c.x, c.y, c.z] = true;
    }

    public int FindDropRow(int x, int z)
    {
        if (x < 0 || x >= xSize || z < 0 || z >= zSize) return -1;
        for (int y = 0; y < ySize; y++)
        {
            if (!_occupied[x, y, z]) return y;
        }
        return -1;
    }

    public Vector3 CellToWorld(Vector3Int cell)
    {
        return gridOrigin + new Vector3(
            (cell.x + 0.5f) * cellSize,
            (cell.y + 0.5f) * cellSize,
            (cell.z + 0.5f) * cellSize
        );
    }

    public Vector3Int WorldToCell(Vector3 worldPos)
    {
        Vector3 local = worldPos - gridOrigin;
        return new Vector3Int(
            Mathf.FloorToInt(local.x / cellSize),
            0,
            Mathf.FloorToInt(local.z / cellSize)
        );
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 0, 0.2f);
        for (int x = 0; x < xSize; x++)
            for (int y = 0; y < ySize; y++)
                for (int z = 0; z < zSize; z++)
                {
                    var center = gridOrigin + new Vector3(
                        (x + 0.5f) * cellSize,
                        (y + 0.5f) * cellSize,
                        (z + 0.5f) * cellSize
                    );
                    Gizmos.DrawWireCube(center, Vector3.one * cellSize * 0.95f);
                }
    }
}