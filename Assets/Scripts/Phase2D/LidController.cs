using UnityEngine;

public class LidController : MonoBehaviour
{
    [Header("参照")]
    [SerializeField] private GameObject lidObject;
    [SerializeField] private GameObject tapeObject;

    BoxManager _box;

    void Awake()
    {
        _box = GetComponentInParent<BoxManager>();
    }

    /// <summary>蓋を閉じる（プロトタイプは即時表示）</summary>
    public void PlayCloseAnimation()
    {
        if (lidObject  != null) lidObject.SetActive(true);
        if (tapeObject != null) tapeObject.SetActive(true);

        _box.OnCloseAnimationComplete();
    }
}