using UnityEngine;
using UnityEngine.UI;

public class MainGameUIGroup : UIGroup 
{

    [SerializeField] private Text stageText;
    [SerializeField] private Text scoreText;
    [SerializeField] private KnifePanelUI knifePanel;

    private void OnEnable()
    {
        // ������������� �� ������� ��� ��������� UI
        LevelManager.OnLevelStarted += UpdateLevelUI;
        LevelManager.OnShoot += knifePanel.UseKnife;
        LevelManager.OnScoreUpdate += UpdateScoreUI;
    }

    private void OnDisable()
    {
        // ������������ �� ������� ��� ����������� UI
        LevelManager.OnLevelStarted -= UpdateLevelUI;
        LevelManager.OnShoot -= knifePanel.UseKnife;
        LevelManager.OnScoreUpdate -= UpdateScoreUI;
    }

    // ����� ��� ���������� ������
    private void UpdateLevelUI(int level, int knives)
    {
        if (stageText != null)
        {
            stageText.text = "STAGE " + level;
        }
        knifePanel.Initialize(knives);
    }
    private void UpdateScoreUI()
    {
        scoreText.text = LevelManager.Instance.GetScore().ToString();
    }
}
