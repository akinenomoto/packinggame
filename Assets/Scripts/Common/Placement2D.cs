using UnityEngine;
using System.Collections.Generic;

public class Placement2D : MonoBehaviour
{
    [Header("プレビュー色")]
    [SerializeField] private Color validColor   = new(0, 1, 0, 0.4f);
    [SerializeField] private Color invalidColor = new(1, 0, 0, 0.4f);

    GameObject     _preview;
    SpriteRenderer _previewSR;

    public void UpdatePreview(Item2DController item)
    {
        var box = GetBoxUnderMouse();
        if (box == null || box.IsLocked) { HidePreview(); return; }

        var cells     = item.GetWorldCells(box.Grid);
        bool canPlace = box.Grid.CanPlace(cells, item);

        EnsurePreview(item);
        _preview.SetActive(true);
        _previewSR.color = canPlace ? validColor : invalidColor;

        _preview.transform.position   = item.transform.position;
        _preview.transform.rotation   = item.transform.rotation;
        _preview.transform.localScale = item.transform.lossyScale;
    }

    public void HidePreview()
    {
        if (_preview != null) _preview.SetActive(false);
    }

    public BoxManager GetBoxUnderMouse()
    {
        Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var hits = Physics2D.OverlapPointAll(mouseWorld);
        foreach (var hit in hits)
        {
            var box = hit.GetComponentInParent<BoxManager>();
            if (box != null) return box;
        }
        return null;
    }

    void EnsurePreview(Item2DController item)
    {
        if (_preview != null) return;

        _preview          = new GameObject("Placement_Preview");
        _previewSR        = _preview.AddComponent<SpriteRenderer>();
        _previewSR.sprite = item.GetComponent<SpriteRenderer>().sprite;
        _previewSR.color  = validColor;
        _previewSR.sortingOrder = 20;
    }
}