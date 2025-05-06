using UnityEngine;


public class BackToMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject currentPanel;

    public void OnBackButtonClicked()
    {
        if (mainMenuPanel != null) mainMenuPanel.SetActive(true);
        if (currentPanel != null) currentPanel.SetActive(false);
    }
}