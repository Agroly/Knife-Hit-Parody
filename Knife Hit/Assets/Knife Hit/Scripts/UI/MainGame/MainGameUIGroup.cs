using UnityEngine;
using UnityEngine.UI;

public class MainGameUIGroup : UIGroup 
{

    [SerializeField] private Text stageText;
    [SerializeField] private KnifePanelUI knifePanel;

    private void OnEnable()
    {
        // Подписываемся на событие при активации UI
        LevelManager.OnLevelStarted += UpdateUI;
        LevelManager.OnShoot += knifePanel.UseKnife;
    }

    private void OnDisable()
    {
        // Отписываемся от события при деактивации UI
        LevelManager.OnLevelStarted -= UpdateUI;
        LevelManager.OnShoot -= knifePanel.UseKnife;
    }

    // Метод для обновления текста
    private void UpdateUI(int level, int knives)
    {
        if (stageText != null)
        {
            stageText.text = "STAGE " + level;
        }
        knifePanel.Initialize(knives);
    }
}
