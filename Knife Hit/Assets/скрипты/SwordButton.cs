using UnityEngine;

public class SwordClickHandler : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject knifePanel;

    public void OnSwordClicked()
    {
        mainMenuPanel.SetActive(false);
        knifePanel.SetActive(true);
    }
}