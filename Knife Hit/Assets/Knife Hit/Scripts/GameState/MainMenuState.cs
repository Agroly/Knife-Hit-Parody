using UnityEngine;

public class MainMenuState : IGameState
{
    public void Enter()
    {
        UIManager.Instance.ShowMainMenu();
    }

    public void Update()
    {
        // Можно слушать нажатия, или UI вызовет gameManager.ChangeState
    }

    public void Exit()
    {
        Debug.Log("Exiting Main Menu");
        // Скрыть UI
    }
}
