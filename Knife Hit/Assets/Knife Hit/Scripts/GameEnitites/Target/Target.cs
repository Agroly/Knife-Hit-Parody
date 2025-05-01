using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class Target : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private TargetData targetData; 

    private int currentHits = 0;  

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        ApplyTargetData();
    }

    private void Update()
    {
        RotateTarget();
    }

    private void ApplyTargetData()
    {
        if (targetData != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = targetData.targetSprite;
        }
    }

    private void RotateTarget()
    {
        transform.Rotate(Vector3.forward * targetData.rotationSpeed * Time.deltaTime);
    }

    // Метод для обработки попадания ножа
    public void Hit()
    {
        currentHits++;
        if (currentHits >= targetData.knifeHitsRequired)
        {
            DestroyTarget();
        }
    }

    // Метод для уничтожения цели
    private void DestroyTarget()
    {      
        if (targetData.targetType == TargetType.Boss)
        {
            // Специальная логика для босса
            Debug.Log("Boss defeated!");
        }
        Destroy(gameObject);
    }
}