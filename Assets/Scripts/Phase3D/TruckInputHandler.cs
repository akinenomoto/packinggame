using UnityEngine;

public class TruckInputHandler : MonoBehaviour
{
    [Header("Truck Floor Layer")]
    public LayerMask floorLayerMask;

    void Update()
    {
        // R キーで回転
        if (Input.GetKeyDown(KeyCode.R))
        {
            var sel = TruckManager.Instance?.GetSelected();
            sel?.RotateY();
        }

        // 左クリックで床に配置
        if (Input.GetMouseButtonDown(0))
        {
            TryPlace();
        }
    }

    void TryPlace()
    {
    var selected = TruckManager.Instance?.GetSelected();
    if (selected == null) return;

    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    // アイテム自体をクリックした場合は配置しない
    if (Physics.Raycast(ray, out RaycastHit checkHit, 100f))
    {
        if (checkHit.collider.GetComponentInParent<Item3DController>() != null)
            return; // アイテム選択クリックなので無視
    }

    // 床へのRaycast
    if (!Physics.Raycast(ray, out RaycastHit hit, 100f, floorLayerMask)) return;

    var grid = TruckManager.Instance.grid;
    Vector3Int cell = grid.WorldToCell(hit.point);

    int dropY = grid.FindDropRow(cell.x, cell.z);
    if (dropY < 0) { Debug.Log("その列は満杯です"); return; }

    Vector3Int origin = new Vector3Int(cell.x, dropY, cell.z);
    var cells = selected.GetCells(origin);

    if (!grid.CanPlace(cells)) { Debug.Log("配置できません"); return; }

    grid.Place(cells);
    Vector3 worldPos = grid.CellToWorld(origin);
    selected.PlaceAt(worldPos);
    TruckManager.Instance.OnItemPlaced(selected);
    }
}
