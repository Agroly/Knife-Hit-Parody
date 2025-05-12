using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;



[RequireComponent(typeof(KnivesController))]
[RequireComponent(typeof(ObjectSpawner))]
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    private int currentLevel = 1;
    private int currentScore = 0;
    private int availableKnives;
    private Knife currentKnife;
    private Target currentTarget;

    private ObjectSpawner spawner;
    private KnivesController knivesController;

    public static event System.Action<int, int> OnLevelStarted;
    public static event System.Action OnShoot;
    public static event System.Action OnScoreUpdate;

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
        currentTarget = spawner.SpawnTarget(levelIndex);
        currentTarget.Init();
        availableKnives = currentTarget.knifeHitsRequired;

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
            yield return new WaitForSeconds(0.05f);
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
        spawner.DestroyApples();
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

    public void AddScore()
    {
        currentScore++;
        OnScoreUpdate.Invoke();
    }
    public void StartScore()
    {
        currentScore = 0;
        OnScoreUpdate.Invoke();
    }

    public int GetScore() => currentScore;
    public int GetStage() => currentLevel;
}
