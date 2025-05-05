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
        StartCoroutine(AnimateSpawn(knife.gameObject));
        return knife;
    }

    public Target SpawnTarget(TargetData data)
    {
        Target target = Instantiate(targetPrefab, targetSpawnPoint.position, Quaternion.identity);
        target.Initialize(data);
        StartCoroutine(AnimateSpawn(target.gameObject));
        return target;
    }

    private IEnumerator AnimateSpawn(GameObject obj)
    {
        float elapsed = 0f;

        if (obj == null) yield break;

        Vector3 startPos = obj.transform.position + new Vector3(0, -2f, 0);
        Vector3 endPos = obj.transform.position;

        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        if (sr == null) yield break;

        Color color = sr.color;
        color.a = 0f;
        sr.color = color;

        while (elapsed < spawnDuration)
        {
            if (obj == null || sr == null) yield break;

            elapsed += Time.deltaTime;
            float t = elapsed / spawnDuration;

            float smoothT = 1f - Mathf.Pow(1f - t, 3);
            obj.transform.position = Vector3.Lerp(startPos, endPos, smoothT);

            color.a = Mathf.Lerp(0f, 1f, t);
            sr.color = color;

            yield return null;
        }

        if (obj != null && sr != null)
        {
            obj.transform.position = endPos;
            color.a = 1f;
            sr.color = color;
        }
    }

}

