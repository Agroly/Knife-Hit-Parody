using UnityEngine;

public class TitleArcIntro : MonoBehaviour
{
    public RectTransform titleLeft;       // Левый текст
    public RectTransform titleRight;      // Правый текст
    public float moveDuration = 1.0f;     // Время на движение
    public float swingAngle = 5f;         // Угол качания (влево/вправ)
    public float swingSpeed = 2f;         // Скорость качания

    private Vector2 leftStart, rightStart; // Начальные позиции (за экраном)
    private Vector2 centerTarget;         // Центр для встречи
    private float timer = 0f;
    private bool arrived = false;

    void Start()
    {
        // Стартовые позиции: слева и справа (за пределами экрана)
        leftStart = new Vector2(-800f, 0f);  // Левый старт — за пределами слева
        rightStart = new Vector2(800f, 0f); // Правый старт — за пределами справа

        // Центр, куда оба должны встретиться
        centerTarget = new Vector2(0f, 0f); // Центр по оси X, Y 0 для простоты

        // Устанавливаем начальные позиции
        titleLeft.anchoredPosition = leftStart;
        titleRight.anchoredPosition = rightStart;
    }

    void Update()
    {
        if (!arrived)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / moveDuration);

            // Для левого текста: движение в центр
            Vector2 leftPos = Vector2.Lerp(leftStart, centerTarget, t);
            titleLeft.anchoredPosition = leftPos;

            // Для правого текста: движение в центр
            Vector2 rightPos = Vector2.Lerp(rightStart, centerTarget, t);
            titleRight.anchoredPosition = rightPos;

            // Если оба текста достигли центра
            if (t >= 1f)
            {
                arrived = true;
            }
        }
        else
        {
            // После того как они встретились — качаемся влево/вправ
            float angle = Mathf.Sin(Time.time * swingSpeed) * swingAngle;
            titleLeft.localRotation = Quaternion.Euler(0, 0, angle);
            titleRight.localRotation = Quaternion.Euler(0, 0, -angle);
        }
    }
}