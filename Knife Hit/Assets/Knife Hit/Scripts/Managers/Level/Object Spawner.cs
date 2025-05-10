using DG.Tweening;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ObjectSpawner : MonoBehaviour
{

    [Header("Prefabs")]
    [SerializeField] private Knife knifePrefab;

    [Header("Spawn Points")]
    [SerializeField] private Transform knifeSpawnPoint;
    [SerializeField] private Transform targetSpawnPoint;

    [Header("Spawn Animation")]
    [SerializeField] private float spawnDuration = 0.1f;
    [SerializeField] private AudioClip spawnTargetSound;

    [Header("Targets prefabs")]
    [SerializeField] private List<Target> OneToFourTargets;
    [SerializeField] private List<Target> Bosses;
    [SerializeField] private List<Target> SixToNineTargets;

    [Header("Apple Spawn Settings")]
    [SerializeField] private GameObject applePrefab;
    [SerializeField] private int minApples = 3;
    [SerializeField] private int maxApples = 5;
    [SerializeField] private float spawnRadius = 5f;

    private List<GameObject> apples = new List<GameObject>();
    public Knife SpawnKnife()
    {
        Knife knife = Instantiate(knifePrefab, knifeSpawnPoint.position, Quaternion.identity);
        AnimateSpawn(knife.gameObject);
        return knife;
    }

    public Target SpawnTarget(int level)
    {
        Target target = Instantiate(ChooseTarget(level), targetSpawnPoint.position, Quaternion.identity);
        AnimateSpawnTarget(target.gameObject);
        AudioManager.Instance.PlaySFX(spawnTargetSound);
        return target;
    }
    public void SpawnApples(GameObject target)
    {
        int appleCount = Random.Range(minApples, maxApples + 1);
        List<GameObject> apples = new List<GameObject>();
        List<Vector3> usedPositions = new List<Vector3>();

        int attempts = 0;
        int maxAttempts = appleCount * 10; // чтобы избежать вечного цикла

        while (apples.Count < appleCount && attempts < maxAttempts)
        {
            attempts++;

            float angle = Random.Range(0f, 360f);
            float rad = angle * Mathf.Deg2Rad;

            Vector3 spawnPos = targetSpawnPoint.position + new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0f) * spawnRadius;

            // Проверка: не слишком ли близко к уже заспавненным яблокам
            bool tooClose = false;
            foreach (var pos in usedPositions)
            {
                if (Vector3.Distance(spawnPos, pos) < 1f) // минимальное расстояние между яблоками
                {
                    tooClose = true;
                    break;
                }
            }

            if (tooClose) continue;

            Vector2 directionToCenter = (targetSpawnPoint.position - spawnPos).normalized;
            float zAngle = Mathf.Atan2(directionToCenter.y, directionToCenter.x) * Mathf.Rad2Deg + 90f;

            Quaternion rotation = Quaternion.Euler(0f, 0f, zAngle);
            var apple = Instantiate(applePrefab, spawnPos, rotation);

            apple.transform.SetParent(target.transform, true);

            apple.transform.DOScale(Vector3.one, 0.25f)
                .SetEase(Ease.OutBack);

            apples.Add(apple);
            usedPositions.Add(spawnPos);
        }
    }


    public void DestroyApples()
    {
        foreach (var apple in apples)
        {
            Destroy(apple);
        }
    }

    private Target ChooseTarget(int level)
    {
        List<Target> targetList;

        if (level >= 1 && level <= 4)
        {
            targetList = OneToFourTargets;
        }
        else if (level >= 6 && level <= 9)
        {
            targetList = SixToNineTargets;
        }
        else
        {
            targetList = Bosses;
        }
        int randomIndex = Random.Range(0, targetList.Count);
        return targetList[randomIndex];
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

        // Плавное поднятие
        obj.transform.DOMoveY(obj.transform.position.y + 2f, spawnDuration)
            .SetEase(Ease.OutCubic);

        // Плавное появление
        sr.DOFade(1f, spawnDuration);
    }
    private void AnimateSpawnTarget(GameObject obj)
    {
        if (obj == null) return;

        obj.transform.localScale = Vector3.zero;

        obj.transform.DOScale(Vector3.one, spawnDuration)
        .SetEase(Ease.OutBack)
        .OnComplete(() => SpawnApples(obj));
    }

}

