using UnityEngine;

public class SwordDrop : MonoBehaviour
{
    public float riseDuration = 1.0f;
    public float startYOffset = -1500f; // Ниже центра
    public GameObject start;           // Твоя кнопка "Start"

    private RectTransform rt;
    private Vector3 startPos;
    private Vector3 targetPos;
    private float time = 0f;
    private bool moving = true;

    void Start()
    {
        rt = GetComponent<RectTransform>();
        targetPos = rt.anchoredPosition;
        startPos = targetPos + new Vector3(0, startYOffset, 0);
        rt.anchoredPosition = startPos;

        if (start != null)
            start.SetActive(false); // Скрыть кнопку в начале
    }

    void Update()
    {
        if (!moving) return;

        time += Time.deltaTime;
        float t = Mathf.Clamp01(time / riseDuration);
        t = Mathf.SmoothStep(0, 1, t);

        rt.anchoredPosition = Vector3.Lerp(startPos, targetPos, t);

        if (t >= 1f)
        {
            moving = false;
            Invoke("ShowStartButton", 1f); // Задержка перед появлением
        }
    }

    void ShowStartButton()
    {
        if (start != null)
        {
            start.SetActive(true);
            var popAnim = start.GetComponent<StartButton>();
            if (popAnim != null)
                popAnim.enabled = true;
        }
    }
}