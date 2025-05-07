using DG.Tweening;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{

    [Header("Prefabs")]
    [SerializeField] private Knife knifePrefab;
    [SerializeField] private Target targetPrefab;

    [Header("Spawn Points")]
    [SerializeField] private Transform knifeSpawnPoint;
    [SerializeField] private Transform targetSpawnPoint;

    [Header("Spawn Animation")]
    [SerializeField] private float spawnDuration = 0.1f;



    public Knife SpawnKnife()
    {
        Knife knife = Instantiate(knifePrefab, knifeSpawnPoint.position, Quaternion.identity);
        AnimateSpawn(knife.gameObject);
        return knife;
    }

    public Target SpawnTarget(TargetData data)
    {
        Target target = Instantiate(targetPrefab, targetSpawnPoint.position, Quaternion.identity);
        target.Initialize(data);
        AnimateSpawnTarget(target.gameObject);
        return target;
    }

    private void AnimateSpawn(GameObject obj)
    {
        if (obj == null) return;

        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        if (sr == null) return;

        Vector3 startPos = obj.transform.position + new Vector3(0, -2f, 0);
        obj.transform.position = startPos;

        Color startColor = sr.color;
        startColor.a = 0f;
        sr.color = startColor;

        // ѕлавное подн€тие
        obj.transform.DOMoveY(obj.transform.position.y + 2f, spawnDuration)
            .SetEase(Ease.OutCubic);

        // ѕлавное по€вление
        sr.DOFade(1f, spawnDuration);
    }
    private void AnimateSpawnTarget(GameObject obj)
    {
        if (obj == null) return;

        obj.transform.localScale = Vector3.zero;

        obj.transform.DOScale(Vector3.one, spawnDuration)
            .SetEase(Ease.OutBack); // Ёффект Ђлопающегос€ пузыр€ї
    }

}

