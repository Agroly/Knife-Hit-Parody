using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



[RequireComponent(typeof(KnivesController))]
[RequireComponent(typeof(ObjectSpawner))]
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    [Header("Data variations")]
    [SerializeField] private List<TargetData> regularTargets;
    [SerializeField] private List<TargetData> bossTargets;


    private int currentLevel = 1;
    private int availableKnives;
    private Knife currentKnife;
    private Target currentTarget;

    private ObjectSpawner spawner;
    private KnivesController knivesController;

    public static event System.Action<int, int> OnLevelStarted;
    public static event System.Action OnShoot;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;

        spawner = GetComponent<ObjectSpawner>();
        knivesController = GetComponent<KnivesController>();
    }


    public void StartLevel(int levelIndex)
    {
        currentLevel = levelIndex;
        TargetData selectedData = ChooseTargetData(levelIndex);
        currentTarget = spawner.SpawnTarget(selectedData);
        availableKnives = selectedData.knifeHitsRequired;
        currentKnife = spawner.SpawnKnife();
        knivesController.RegisterKnife(currentKnife);

        OnLevelStarted?.Invoke(currentLevel, availableKnives);
    }
    public IEnumerator ShootAndSpawn()
    {
        if (currentKnife != null)
        {
            currentKnife.Shoot();
            currentKnife = null;
            availableKnives -= 1;
            OnShoot.Invoke();
            yield return new WaitForSeconds(0.1f);
            if (availableKnives > 0)
            {
                currentKnife = spawner.SpawnKnife();
                knivesController.RegisterKnife(currentKnife);
            }
        }
    }

    public void CompleteLevel()
    {
        knivesController.DestroyStuckKnives(new Vector2 (0,0));
        StartCoroutine(NextLevel());
    }
    public void ExitGame()
    {
        knivesController.DestroyAllKnives();
        Destroy(currentTarget.gameObject);
    }
    private IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(0.5f);
        StartLevel(currentLevel+1);
    }

    private TargetData ChooseTargetData(int level)
    {
        if (level % 5 == 0)
            return bossTargets[Random.Range(0, bossTargets.Count)];
        else
            return regularTargets[Random.Range(0, regularTargets.Count)];
    }
}
