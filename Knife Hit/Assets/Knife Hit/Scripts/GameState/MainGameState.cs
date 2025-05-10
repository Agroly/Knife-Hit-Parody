using System.Runtime.Serialization;
using UnityEngine;

public class MainGameState : IGameState
{
    public void Enter()
    {
        UIManager.Instance.ShowMainGame();
        LevelManager.Instance.StartLevel(1);
        LevelManager.Instance.StartScore();
        InputManager.Instance.ListenToTouch();
        // ����������� ����, �������� HUD � �.�.
    }

    public void Update()
    {
        // �������� ������ ����
    }

    public void Exit()
    {
        InputManager.Instance.StopListening();
        LevelManager.Instance.ExitGame();
        Debug.Log("Exiting Main Game");
        // ������� HUD � �.�.
    }
}
