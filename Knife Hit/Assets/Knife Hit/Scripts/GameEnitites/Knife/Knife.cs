using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Knife : MonoBehaviour
{
    
    [SerializeField] private KnifeSkinDataSO skin;
    [SerializeField] private float shootForce = 10f;
    [SerializeField] private BoxCollider2D bladeCollider;
    [SerializeField] private BoxCollider2D handleCollider;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    public bool IsStuck { get; private set; }

    private void Awake()
    {
        DisableColliders();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();  // Получаем компонент Rigidbody2D
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsStuck)
        {
            if (collision.gameObject.TryGetComponent<Target>(out var target))
            {
                StickToTarget(target.transform);
                target.Hit();
            }
            else StartCoroutine(BounceAndLose());
        }
    }

    private void StickToTarget(Transform targetTransform)
    {
        bladeCollider.enabled = false;
        handleCollider.enabled = true;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.bodyType = RigidbodyType2D.Kinematic;
        transform.SetParent(targetTransform);
        IsStuck = true;
    }

    public void Shoot()
    {
        if (rb != null && !IsStuck)
        {
            bladeCollider.enabled = true;
            rb.linearVelocity = Vector2.zero;  // Обнуляем текущую скорость
            rb.AddForce(Vector2.up * shootForce, ForceMode2D.Impulse);  // Применяем силу вверх
        }
    }
    private IEnumerator BounceAndLose()
    {
        bladeCollider.enabled = false;
        handleCollider.enabled = false;

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
