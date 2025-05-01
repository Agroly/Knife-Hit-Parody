using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnivesController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float explosionForceMin = 10f;
    [SerializeField] private float explosionForceMax = 20f;
    [SerializeField] private float destroyDelay = 1f;

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

    public IEnumerator DestroyStuckKnives()
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
            knife.transform.parent = null;
            float explosionForce = Random.Range(explosionForceMin, explosionForceMax);
            var rb = knife.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
                rb.gravityScale = 10;
                rb.AddForce(Random.insideUnitCircle * explosionForce, ForceMode2D.Impulse);
            }
        }

        yield return new WaitForSeconds(destroyDelay);

        // ”ничтожаем застр€вшие ножи
        foreach (var knife in stuckKnivesToDestroy)
        {
            if (knife != null)
            {
                Destroy(knife.gameObject);
            }
        }
    }

}