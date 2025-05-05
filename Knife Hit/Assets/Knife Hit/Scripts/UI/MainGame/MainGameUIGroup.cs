using UnityEngine;
using UnityEngine.UI;

public class MainGameUIGroup : UIGroup 
{

    [SerializeField] private Text stageText;
    [SerializeField] private KnifePanelUI knifePanel;

    private void OnEnable()
    {
        // ������������� �� ������� ��� ��������� UI
        LevelManager.OnLevelStarted += UpdateUI;
        LevelManager.OnShoot += knifePanel.UseKnife;
    }

    private void OnDisable()
    {
        // ������������ �� ������� ��� ����������� UI
        LevelManager.OnLevelStarted -= UpdateUI;
        LevelManager.OnShoot -= knifePanel.UseKnife;
    }

    // ����� ��� ���������� ������
    private void UpdateUI(int level, int knives)
    {
        if (stageText != null)
        {
            stageText.text = "STAGE " + level;
        }
        knifePanel.Initialize(knives);
    }
}
