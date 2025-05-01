using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent (typeof(Collider2D))]
public class Knife : MonoBehaviour
{
    
    [SerializeField] private KnifeSkinDataSO skin;
    [SerializeField] private float shootForce = 10f;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    public bool IsStuck { get; private set; }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();  // Получаем компонент Rigidbody2D
    }

    private void Start()
    {
        ApplySkin();
        Shoot();
    }
    IEnumerator Shooting()
    {
        Shoot();
        yield return 1f;
    }

    public void SetSkin(KnifeSkinDataSO newSkin)
    {
        skin = newSkin;
        ApplySkin();
    }

    private void ApplySkin()
    {
        if (skin != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = skin.knifeSprite;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Target>(out var target))
        {
            StickToTarget(target.transform);
            target.Hit();
        }
    }

    private void StickToTarget(Transform targetTransform)
    {
        GetComponent<Collider2D>().enabled = false;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.bodyType = RigidbodyType2D.Kinematic;
        transform.SetParent(targetTransform);
        IsStuck = true;
    }

    public void Shoot()
    {
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;  // Обнуляем текущую скорость
            rb.AddForce(Vector2.up * shootForce, ForceMode2D.Impulse);  // Применяем силу вверх
        }
    }
}
