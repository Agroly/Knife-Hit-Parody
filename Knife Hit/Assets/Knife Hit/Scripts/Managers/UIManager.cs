using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("UI Groups")]
    [SerializeField] private UIGroup mainMenu;
    [SerializeField] private UIGroup mainGame;
    [SerializeField] private UIGroup gameOver;

    private UIGroup current;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void SwitchTo(UIGroup newGroup)
    {
        if (current != null)
        {
            current.Hide();
        }

        current = newGroup;
        current.Show();
    }

    // Специальные методы для конкретных UI-групп (опционально)
    public void ShowMainMenu() => SwitchTo(mainMenu);
    public void ShowMainGame() => SwitchTo(mainGame);
    public void ShowGameOver() => SwitchTo(gameOver);
}
