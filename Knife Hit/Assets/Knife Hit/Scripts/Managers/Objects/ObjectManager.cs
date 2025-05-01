using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager Instance { get; private set; }

    [Header("Prefabs")]
    [SerializeField] private Knife knifePrefab;
    [SerializeField] private Target targetPrefab;

    [Header("Spawn Points")]
    [SerializeField] private Transform knifeSpawnPoint;
    [SerializeField] private Transform targetSpawnPoint;

    private Knife currentKnife;
    private Target currentTarget;
    public KnivesController knivesController;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }     
        Instance = this;
    }

    public IEnumerator SpawnKnives()
    {
        while (true)
        {
            SpawnKnife();
            yield return new WaitForSeconds(1f);
        }
    }
    public void SpawnKnife()
    {
        currentKnife = Instantiate(knifePrefab, knifeSpawnPoint.position, Quaternion.identity);
        knivesController.RegisterKnife(currentKnife);
    }

    public void SpawnTarget()
    {
        currentTarget = Instantiate(targetPrefab, targetSpawnPoint.position, Quaternion.identity);
    }
    public void DestroyAll()
    {
        if (currentTarget != null)
        {
            Destroy(currentTarget.gameObject);
        }

        knivesController.DestroyAllKnives();
    }
    public void SpawnLevelObjects()
    {
        SpawnTarget();
        SpawnKnife();
    }
}

