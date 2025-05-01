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

    // ����� ��� ��������� ��������� ����
    public void Hit()
    {
        currentHits++;
        if (currentHits >= targetData.knifeHitsRequired)
        {
            DestroyTarget();
        }
    }

    // ����� ��� ����������� ����
    private void DestroyTarget()
    {      
        if (targetData.targetType == TargetType.Boss)
        {
            // ����������� ������ ��� �����
            Debug.Log("Boss defeated!");
        }
        Destroy(gameObject);
    }
}