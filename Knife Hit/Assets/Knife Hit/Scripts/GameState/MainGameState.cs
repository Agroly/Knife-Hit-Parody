using System.Runtime.Serialization;
using UnityEngine;

public class MainGameState : IGameState
{
    public void Enter()
    {
        UIManager.Instance.ShowMainGame();
        ObjectManager.Instance.StartCoroutine(ObjectManager.Instance.SpawnKnives());
        ObjectManager.Instance.SpawnTarget();
        // ����������� ����, �������� HUD � �.�.
    }

    public void Update()
    {
        // �������� ������ ����
    }

    public void Exit()
    {
        ObjectManager.Instance.StopAllCoroutines();
        ObjectManager.Instance.DestroyAll();
        Debug.Log("Exiting Main Game");
        // ������� HUD � �.�.
    }
}
