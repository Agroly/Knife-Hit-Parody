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
        // �������� ������ ����
    }

    public void Exit()
    {
        
    }
}
