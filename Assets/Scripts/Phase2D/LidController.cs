using UnityEngine;

public class LidController : MonoBehaviour
{
    [Header("参照")]
    [SerializeField] private GameObject lidObject;
    [SerializeField] private GameObject tapeObject;
    [SerializeField] private GameObject bodyObject;  // ← 追加

    BoxManager _box;

    void Awake()
    {
        _box = GetComponentInParent<BoxManager>();
    }

    public void PlayCloseAnimation()
    {
        if (bodyObject != null) bodyObject.SetActive(false);  // ← 追加
        if (lidObject  != null) lidObject.SetActive(true);
        if (tapeObject != null) tapeObject.SetActive(true);

        _box.OnCloseAnimationComplete();
    }
}