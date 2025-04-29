using UnityEngine;

public class MainGameState : IGameState
{
    public void Enter()
    {
        UIManager.Instance.ShowMainGame();
        // Подготовить игру, включить HUD и т.д.
    }

    public void Update()
    {
        // Основная логика игры
    }

    public void Exit()
    {
        Debug.Log("Exiting Main Game");
        // Очистка HUD и т.п.
    }
}
