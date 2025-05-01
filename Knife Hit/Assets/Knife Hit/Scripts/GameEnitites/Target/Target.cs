using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class Target : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private TargetData targetData; 

    private int currentHits = 0;
    private float destructionTime = 0.3f;

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
        Debug.Log("Hit");
        currentHits++;
        if (currentHits >= targetData.knifeHitsRequired)
        {
            StartCoroutine(DestroyTarget());
        }
    }

    // Метод для уничтожения цели
    IEnumerator DestroyTarget()
    {      
        if (targetData.targetType == TargetType.Boss)
        {
            // Специальная логика для босса
            Debug.Log("Boss defeated!");
        }
        spriteRenderer.sprite = null;
        StartCoroutine(ObjectManager.Instance.knivesController.DestroyStuckKnives());
        yield return new WaitForSeconds(destructionTime);
        ObjectManager.Instance.SpawnTarget();
        Destroy(gameObject);
    }
}