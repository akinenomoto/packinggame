using System.Collections.Generic;
using UnityEngine;

public class Item3DController : MonoBehaviour
{
    [Header("Shape")]
    public Vector3Int[] baseShape = { new Vector3Int(0, 0, 0) };

    public bool IsPlaced { get; private set; } = false;

    Renderer[] _renderers;
    Color[] _originalColors;
    int _rotY = 0; // 0,1,2,3 × 90°

    void Awake()
    {
        _renderers = GetComponentsInChildren<Renderer>();
        _originalColors = new Color[_renderers.Length];
        for (int i = 0; i < _renderers.Length; i++)
            _originalColors[i] = _renderers[i].material.color;
    }

    void OnMouseDown()
    {
        if (IsPlaced) return;
        TruckManager.Instance?.SelectItem(this);
    }

    public void SetHighlight(bool on)
    {
        for (int i = 0; i < _renderers.Length; i++)
            _renderers[i].material.color = on ? Color.yellow : _originalColors[i];
    }

    public void RotateY()
    {
        _rotY = (_rotY + 1) % 4;
        transform.rotation = Quaternion.Euler(0, _rotY * 90f, 0);
    }

    public void PlaceAt(Vector3 worldPos)
    {
        transform.position = worldPos;
        IsPlaced = true;
        SetHighlight(false);
    }

    public List<Vector3Int> GetCells(Vector3Int origin)
    {
        var cells = new List<Vector3Int>();
        foreach (var offset in baseShape)
        {
            Vector3Int rot = RotateOffset(offset, _rotY);
            cells.Add(origin + rot);
        }
        return cells;
    }

    static Vector3Int RotateOffset(Vector3Int v, int times)
    {
        for (int i = 0; i < times; i++)
            v = new Vector3Int(v.z, v.y, -v.x);
        return v;
    }
}
