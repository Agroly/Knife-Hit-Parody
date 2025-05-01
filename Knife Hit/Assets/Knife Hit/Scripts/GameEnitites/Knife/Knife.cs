using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent (typeof(Collider2D))]
public class Knife : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private KnifeSkin skin;
    private Rigidbody2D rb;

    [SerializeField] private float shootForce = 10f;  

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

    public void SetSkin(KnifeSkin newSkin)
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
            ContactPoint2D contact = collision.GetContact(0);
            StickToTarget(target.transform, contact.point);
            target.Hit();
        }
    }

    private void StickToTarget(Transform targetTransform, Vector2 contactPoint)
    {
        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        GetComponent<Collider2D>().enabled = false;

        // Направление от центра цели к точке удара
        Vector2 direction = (Vector2)targetTransform.position - (Vector2)contactPoint;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Поворот ножа так, чтобы он был перпендикулярен краю цели
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);

        // Прикрепляем нож к цели
        transform.SetParent(targetTransform);
    }

    public void Shoot()
    {
        if (rb != null)
        {
            // Направляем нож вверх, при этом добавляем силу к Rigidbody2D
            rb.linearVelocity = Vector2.zero;  // Обнуляем текущую скорость
            rb.AddForce(Vector2.up * shootForce, ForceMode2D.Impulse);  // Применяем силу вверх
        }
    }
}
