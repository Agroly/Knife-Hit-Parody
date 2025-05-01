using System.Runtime.Serialization;
using UnityEngine;

public class MainGameState : IGameState
{
    public void Enter()
    {
        UIManager.Instance.ShowMainGame();
        ObjectManager.Instance.StartCoroutine(ObjectManager.Instance.SpawnKnives());
        ObjectManager.Instance.SpawnTarget();
        // Подготовить игру, включить HUD и т.д.
    }

    public void Update()
    {
        // Основная логика игры
    }

    public void Exit()
    {
        ObjectManager.Instance.StopAllCoroutines();
        ObjectManager.Instance.DestroyAll();
        Debug.Log("Exiting Main Game");
        // Очистка HUD и т.п.
    }
}
