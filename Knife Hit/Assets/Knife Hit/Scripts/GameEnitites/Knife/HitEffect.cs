using UnityEngine;
using DG.Tweening;

public class HitEffect : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField] float duration = 1f;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void Start()
    {
        sr.color = new Color(0.5f, 0.5f, 0.5f, 1f); // סונûי ס אכüפמי
        transform.localScale = Vector3.zero;

        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScale(1f, duration));
        seq.Join(sr.DOFade(0f, duration).SetEase(Ease.OutBounce));
        seq.OnComplete(() => Destroy(gameObject));
    }
}
