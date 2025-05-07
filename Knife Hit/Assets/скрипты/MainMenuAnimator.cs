using UnityEngine;

public class MainMenuAnimator : MonoBehaviour
{
    [SerializeField] private RectTransform[] topElements;     // Всё, что уезжает вверх
    [SerializeField] private RectTransform[] bottomElements;  // Всё, что уезжает вниз
    [SerializeField] private GameObject nextPanel;            // Следующая панель или UI

    [SerializeField] private float moveDistance = 1000f;
    [SerializeField] private float moveSpeed = 800f;

    private bool isAnimating = false;

    public void OnStartClicked()
    {
        isAnimating = true;
    }

    void Update()
    {
        if (!isAnimating) return;

        bool allFinished = true;

        foreach (var element in topElements)
        {
            if (element != null)
            {
                Vector2 target = element.anchoredPosition + Vector2.up * moveDistance;
                element.anchoredPosition = Vector2.MoveTowards(element.anchoredPosition, target, moveSpeed * Time.deltaTime);
                if (Vector2.Distance(element.anchoredPosition, target) > 1f)
                    allFinished = false;
            }
        }

        foreach (var element in bottomElements)
        {
            if (element != null)
            {
                Vector2 target = element.anchoredPosition + Vector2.down * moveDistance;
                element.anchoredPosition = Vector2.MoveTowards(element.anchoredPosition, target, moveSpeed * Time.deltaTime);
                if (Vector2.Distance(element.anchoredPosition, target) > 1f)
                    allFinished = false;
            }
        }

        if (allFinished)
        {
            isAnimating = false;
            if (nextPanel != null)
            {
                nextPanel.SetActive(true);
                gameObject.SetActive(false); // Отключить текущее меню
            }
        }
    }
}