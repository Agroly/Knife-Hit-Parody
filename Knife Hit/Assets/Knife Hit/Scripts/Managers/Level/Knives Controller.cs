using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnivesController : MonoBehaviour
{
    [Header("Destroy Stuck Settings")]
    [SerializeField] private float explosionForceMin = 10f;
    [SerializeField] private float explosionForceMax = 20f;
    [SerializeField] private float destroyDelay = 1f;
    [SerializeField] private float gravityScale = 5f;

    private readonly List<Knife> allKnives = new List<Knife>();

    public void RegisterKnife(Knife knife)
    {
        if (!allKnives.Contains(knife))
        {
            allKnives.Add(knife);
        }
    }

    public void DestroyAllKnives()
    {
        var knivesToProcess = new List<Knife>(allKnives);

        allKnives.Clear();

        foreach (var knife in knivesToProcess)
        {
            Destroy(knife.gameObject);
        };
    }

    public void DestroyStuckKnives(Vector2 targetPos)
    {
        List<Knife> stuckKnivesToDestroy = new List<Knife>();

        foreach (var knife in allKnives)
        {
            if (knife != null && knife.IsStuck)
            {
                stuckKnivesToDestroy.Add(knife);
            }
        }
        

        foreach (var knife in stuckKnivesToDestroy)
        {
            allKnives.Remove(knife);
            Destroy(knife.gameObject,destroyDelay);
            knife.transform.parent = null;
            knife.DisableColliders();

            float explosionForce = Random.Range(explosionForceMin, explosionForceMax);
            var rb = knife.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
                rb.gravityScale = gravityScale;

                // Вычисляем направление от центра взрыва до ножа
                Vector2 direction = ((Vector2)knife.transform.position - targetPos).normalized;

                direction.y += Random.Range(-0.5f, +0.5f);
                direction = direction.normalized;

                float force = Random.Range(explosionForceMin, explosionForceMax);
                rb.AddForce(direction * force, ForceMode2D.Impulse);

                // Опционально: добавим вращение
                float torque = Random.Range(0, 5);
                rb.AddTorque(torque, ForceMode2D.Impulse);


            }
        }
    }
}