using UnityEngine;

public class MainGameState : IGameState
{
    public void Enter()
    {
        UIManager.Instance.ShowMainGame();
        // ����������� ����, �������� HUD � �.�.
    }

    public void Update()
    {
        // �������� ������ ����
    }

    public void Exit()
    {
        Debug.Log("Exiting Main Game");
        // ������� HUD � �.�.
    }
}
