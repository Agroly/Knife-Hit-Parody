using System.Collections;
using UnityEngine;

public class GameObjectsController : MonoBehaviour
{
    public static GameObjectsController Instance { get; private set; }

    [Header("Prefabs")]
    [SerializeField] private Knife knifePrefab;
    [SerializeField] private Target targetPrefab;

    [Header("Spawn Points")]
    [SerializeField] private Transform knifeSpawnPoint;
    [SerializeField] private Transform targetSpawnPoint;

    private Knife currentKnife;
    private Target currentTarget;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }     
        Instance = this;
    }
    private void Start()
    {
        StartCoroutine(SpawnKnives());
    }

    IEnumerator SpawnKnives()
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

    }

    public void SpawnTarget()
    {
        currentTarget = Instantiate(targetPrefab, targetSpawnPoint.position, Quaternion.identity);
    }

    public void SpawnLevelObjects()
    {
        SpawnTarget();
        SpawnKnife();
    }
}

