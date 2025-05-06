using System.Runtime.Serialization;
using UnityEngine;

public class MainGameState : IGameState
{
    public void Enter()
    {
        UIManager.Instance.ShowMainGame();
        LevelManager.Instance.StartLevel(1);
        InputManager.Instance.ListenToTouch();
        // Подготовить игру, включить HUD и т.д.
    }

    public void Update()
    {
        // Основная логика игры
    }

    public void Exit()
    {
        InputManager.Instance.StopListening();
        LevelManager.Instance.ExitGame();
        Debug.Log("Exiting Main Game");
        // Очистка HUD и т.п.
    }
}
