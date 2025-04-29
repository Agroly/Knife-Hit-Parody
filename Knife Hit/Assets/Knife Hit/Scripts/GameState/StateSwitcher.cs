using UnityEngine;

public class StateSwitcher : MonoBehaviour
{
    [SerializeField] private GameStateType gameState;
    protected void StateSwitch()
    {
        GameManager.Instance.ChangeState(gameState);
    }
}
