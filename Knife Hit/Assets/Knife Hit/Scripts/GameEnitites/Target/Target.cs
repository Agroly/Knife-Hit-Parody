using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(TargetRotationController))]
public abstract class Target : MonoBehaviour
{
    public TargetRotationController rotationController;

    public int knifeHitsRequired { get; protected set; }
    [SerializeField] private AudioClip hitClip;
    [SerializeField] private AudioClip destroyClip;

    private int currentHits = 0;
    private SpriteRenderer spriteRenderer;


    public virtual void Init()
    {
        rotationController.Rotate(transform);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    // Метод для обработки попадания ножа
    public void Hit()
    {
        HitAnimation();
        currentHits++;
        LevelManager.Instance.AddScore();
        if (currentHits >= knifeHitsRequired)
        {
            DestroyTarget();
            AudioManager.Instance.PlaySFX(destroyClip);
            return;
        }
        AudioManager.Instance.PlaySFX(hitClip);
    }
    private void DestroyTarget()
    {
        spriteRenderer.gameObject.SetActive(false);

        LevelManager.Instance.CompleteLevel();
        Destroy(gameObject, 0.5f);
        return;
    }
    private void HitAnimation()
    {
        transform.DOPunchPosition(Vector3.up * 0.1f, 0.1f);
        spriteRenderer.DOColor(new Color(0.9f, 0.9f, 0.9f), 0.08f)
           .SetLink(gameObject)
           .OnComplete(() => {
               spriteRenderer.DOColor(Color.white, 0.02f).SetLink(gameObject);
           });
    }
}

