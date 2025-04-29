using UnityEngine;

public class GameOverState : IGameState
{

    public void Enter()
    {
        UIManager.Instance.ShowGameOver();
        // Показать экран окончания игры
    }

    public void Update()
    {
        // Ожидать нажатие "перезапуск" или "в меню"
    }

    public void Exit()
    {
        Debug.Log("Exiting Game Over");
        // Скрыть UI
    }
}   
