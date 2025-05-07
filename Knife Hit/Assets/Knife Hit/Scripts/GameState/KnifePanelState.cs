using System.Runtime.Serialization;
using UnityEngine;

public class KnifePanelState : IGameState
{
    public void Enter()
    {
        UIManager.Instance.ShowKnifePanel();
    }

    public void Update()
    {
        // Основная логика игры
    }

    public void Exit()
    {
        
    }
}
