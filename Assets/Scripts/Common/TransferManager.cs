using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferManager : MonoBehaviour
{
    public static TransferManager Instance { get; private set; }

    public float clearDelay = 1.5f;

    readonly List<BoxManager> _closedBoxes = new();

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void RegisterClosedBox(BoxManager box)
    {
        if (!_closedBoxes.Contains(box))
            _closedBoxes.Add(box);
        Debug.Log($"[TransferManager] 梱包完了: Box {box.BoxID}");
    }

    public void BuildTruckItems()
    {
        StartCoroutine(DelayedClear());
    }

    IEnumerator DelayedClear()
    {
        yield return new WaitForSeconds(clearDelay);
        GameManager.Instance?.OnTruckLoadingComplete();
    }
}