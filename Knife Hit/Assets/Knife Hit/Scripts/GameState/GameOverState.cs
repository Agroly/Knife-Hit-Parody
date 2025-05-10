using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class GameOverState : IGameState
{

    public void Enter()
    {
        UIManager.Instance.ShowGameOver();
        UpdateHighScore();
        UpdateHighStage();
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
    private void UpdateHighScore() 
    {
        if (CheckHighScore()) PlayerPrefs.SetInt("highscore", LevelManager.Instance.GetScore());
    }
    private void UpdateHighStage()
    {
        if (CheckHighStage()) PlayerPrefs.SetInt("highstage", LevelManager.Instance.GetStage());
    }

    private bool CheckHighScore() => LevelManager.Instance.GetScore() > PlayerPrefs.GetInt("highscore") ? true : false;
    private bool CheckHighStage() => LevelManager.Instance.GetStage() > PlayerPrefs.GetInt("highstage") ? true : false;

}   
