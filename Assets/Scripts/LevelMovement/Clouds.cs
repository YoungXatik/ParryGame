using DG.Tweening;
using UnityEngine;

public class Clouds : MonoBehaviour
{
    public GameObject target;
    public float duration;

    private void Start()
    {
        var Seq = DOTween.Sequence();
        Seq.Append(gameObject.transform.DOMove(target.transform.position, duration));
        Seq.SetLoops(-1, LoopType.Yoyo);
    }
}
