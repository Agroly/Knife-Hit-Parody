using DG.Tweening;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class Target : MonoBehaviour
{

    [SerializeField] private TargetData targetData;

    private int currentHits = 0;

    private Color originalColor;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    private void Update()
    {
        RotateTarget();
    }
    public void Initialize(TargetData data)
    {
        targetData = data;
    }
    public int GetRequiredKnives() => targetData.knifeHitsRequired;
    private void RotateTarget()
    {
        transform.Rotate(Vector3.forward * targetData.rotationSpeed * Time.deltaTime);
    }

    // Метод для обработки попадания ножа
    public void Hit()
    {
        HitAnimation();
        currentHits++;
        if (currentHits >= targetData.knifeHitsRequired)
        {

            DestroyTarget();
            AudioManager.Instance.PlaySFX(targetData.destroyClip);
            return;
        }
        AudioManager.Instance.PlaySFX(targetData.hitClip);
    }
    private void DestroyTarget()
    {
        spriteRenderer.gameObject.SetActive(false);
        if (targetData.targetType == TargetType.Boss)
        {
            // Специальная логика для босса
            Debug.Log("Boss defeated!");
        }
        LevelManager.Instance.CompleteLevel();
        Destroy(gameObject, 0.5f);
        return;
    }
    private void HitAnimation()
    {
        transform.DOPunchPosition(Vector3.up * 0.1f, 0.1f);
        spriteRenderer.DOColor(new Color(0.9f, 0.9f, 0.9f), 0.08f)
           .OnComplete(() => {
               // Затем возвращаемся к исходному цвету
               spriteRenderer.DOColor(originalColor, 0.02f);
           });
    }
}