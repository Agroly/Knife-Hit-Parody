using UnityEngine;

public class StartButton : MonoBehaviour
{
    public float animationTime = 0.6f;
    private RectTransform rectTransform;
    private Vector3 targetScale = Vector3.one;
    private float timer = 0f;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.localScale = Vector3.zero; // �������� � ����
    }

    void Update()
    {
        if (timer < animationTime)
        {
            timer += Time.deltaTime;
            float t = timer / animationTime;

            // Overshoot ������ (������������)
            float scale = Overshoot(t);
            rectTransform.localScale = new Vector3(scale, scale, scale);
        }
    }

    // ������ ������������ � ��������
    float Overshoot(float t)
    {
        t -= 1f;
        return 1f + (t * t * ((2.5f + 1) * t + 2.5f)); // ���������� 2.5f ��� ���� ������
    }
}
