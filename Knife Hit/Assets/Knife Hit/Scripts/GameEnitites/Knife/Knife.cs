using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Knife : MonoBehaviour
{ 
    [SerializeField] private float shootForce = 10f;
    [SerializeField] private BoxCollider2D bladeCollider;
    [SerializeField] private PolygonCollider2D handleCollider;
    [SerializeField] private ParticleSystem hitParticles;
    [SerializeField] private HitEffect hitEffect;
    [SerializeField] private AudioClip knifeHitSound;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    public bool IsStuck { get; private set; }

    private void Awake()
    {
        DisableColliders();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();  // Получаем компонент Rigidbody2D
    }
    public void SetSelectedSprite()
    {
        spriteRenderer.sprite = KnifeSkinManager.Instance.GetSelectedSprite();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsStuck)
        {
            if (collision.gameObject.TryGetComponent<Target>(out var target))
            {
                StickToTarget(target.transform);
                var hp = Instantiate(hitParticles);
                hp.transform.position = target.transform.position;
                target.Hit();
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!IsStuck)
        {
            Vector3 pos = collision.GetContact(0).point;
            pos.z = 0f;
            Instantiate(hitEffect, pos, Quaternion.identity);
            AudioManager.Instance.PlaySFX(knifeHitSound);
            StartCoroutine(BounceAndLose());
        }
    }

    private void StickToTarget(Transform targetTransform)
    {
        bladeCollider.enabled = false;
        handleCollider.enabled = true;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.bodyType = RigidbodyType2D.Static;
        transform.SetParent(targetTransform);
        IsStuck = true;
    }

    public void Shoot()
    {
        if (rb != null && !IsStuck && enabled)
        {
            bladeCollider.enabled = true;
            rb.linearVelocity = Vector2.zero;  // Обнуляем текущую скорость
            rb.AddForce(Vector2.up * shootForce, ForceMode2D.Impulse);  // Применяем силу вверх
        }
    }
    private IEnumerator BounceAndLose()
    {
        DisableColliders();
        rb.linearVelocity = Vector3.zero;
        rb.gravityScale = 5f;
        rb.AddForce(Vector3.left * Random.Range(-5f, 5f) + Vector3.up * 3f, ForceMode2D.Impulse);

        float randomTorque = Random.Range(10f, 20f);
        randomTorque *= Random.value > 0.5f ? 1 : -1;
        rb.AddTorque(randomTorque, ForceMode2D.Impulse);

        InputManager.Instance.StopListening();

        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.ChangeState(GameStateType.GameOver);
    }
    public void DisableColliders()
    {
        handleCollider.enabled = false;
        bladeCollider.enabled = false;
    }
}
