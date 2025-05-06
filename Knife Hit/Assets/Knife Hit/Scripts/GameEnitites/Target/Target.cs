using System.Collections;
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

    // ����� ��� ��������� ��������� ����
    public void Hit()
    {
        currentHits++;
        if (currentHits >= targetData.knifeHitsRequired)
        {

            DestroyTarget();
        }
    }
    private void DestroyTarget()
    {
        spriteRenderer.gameObject.SetActive(false);
        if (targetData.targetType == TargetType.Boss)
        {
            // ����������� ������ ��� �����
            Debug.Log("Boss defeated!");
        }
        LevelManager.Instance.CompleteLevel();
        Destroy(gameObject, 0.5f);
        return;
    }
}