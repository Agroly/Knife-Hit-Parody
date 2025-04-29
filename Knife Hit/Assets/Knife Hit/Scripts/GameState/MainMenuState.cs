using UnityEngine;

public class MainMenuState : IGameState
{
    public void Enter()
    {
        UIManager.Instance.ShowMainMenu();
    }

    public void Update()
    {
        // ����� ������� �������, ��� UI ������� gameManager.ChangeState
    }

    public void Exit()
    {
        Debug.Log("Exiting Main Menu");
        // ������ UI
    }
}
