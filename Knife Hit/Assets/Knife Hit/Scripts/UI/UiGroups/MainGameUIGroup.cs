using UnityEngine;
using UnityEngine.UI;

public class MainGameUIGroup : UIGroup 
{

    [SerializeField] private Text stageText;

    private void OnEnable()
    {
        // ������������� �� ������� ��� ��������� UI
        LevelManager.OnLevelStarted += UpdateStageText;
    }

    private void OnDisable()
    {
        // ������������ �� ������� ��� ����������� UI
        LevelManager.OnLevelStarted -= UpdateStageText;
    }

    // ����� ��� ���������� ������
    private void UpdateStageText(int level)
    {
        if (stageText != null)
        {
            stageText.text = "Stage " + level;
        }
    }
}
