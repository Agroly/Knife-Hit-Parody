using UnityEngine;

public enum GameStateType
{
    MainMenu,
    MainGame,
    GameOver
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private IGameState currentState;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        ChangeState(new MainMenuState());
    }

    private void Update()
    {
        currentState?.Update();
    }

    private void ChangeState(IGameState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }
    public void ChangeState(GameStateType gameStateType)
    {
        ChangeState(GetStateByType(gameStateType));
    }

    private IGameState GetStateByType(GameStateType stateType)
    {
        switch (stateType)
        {
            case GameStateType.MainMenu:
                return new MainMenuState();
            case GameStateType.MainGame:
                return new MainGameState();
            case GameStateType.GameOver:
                return new GameOverState();
            default:
                Debug.LogError("State type not found: " + stateType);
                return null;
        }
    }
}
