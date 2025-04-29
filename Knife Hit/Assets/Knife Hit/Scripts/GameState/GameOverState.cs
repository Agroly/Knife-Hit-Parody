using UnityEngine;

public class GameOverState : IGameState
{

    public void Enter()
    {
        UIManager.Instance.ShowGameOver();
        // �������� ����� ��������� ����
    }

    public void Update()
    {
        // ������� ������� "����������" ��� "� ����"
    }

    public void Exit()
    {
        Debug.Log("Exiting Game Over");
        // ������ UI
    }
}   
