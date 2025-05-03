using UnityEngine;
using UnityEngine.UI;

public class MainGameUIGroup : UIGroup 
{

    [SerializeField] private Text stageText;

    private void OnEnable()
    {
        // Подписываемся на событие при активации UI
        LevelManager.OnLevelStarted += UpdateStageText;
    }

    private void OnDisable()
    {
        // Отписываемся от события при деактивации UI
        LevelManager.OnLevelStarted -= UpdateStageText;
    }

    // Метод для обновления текста
    private void UpdateStageText(int level)
    {
        if (stageText != null)
        {
            stageText.text = "Stage " + level;
        }
    }
}
