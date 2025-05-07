using UnityEngine;

public class TitleArcIntro : MonoBehaviour
{
    public RectTransform titleLeft;       // ����� �����
    public RectTransform titleRight;      // ������ �����
    public float moveDuration = 1.0f;     // ����� �� ��������
    public float swingAngle = 5f;         // ���� ������� (�����/�����)
    public float swingSpeed = 2f;         // �������� �������

    private Vector2 leftStart, rightStart; // ��������� ������� (�� �������)
    private Vector2 centerTarget;         // ����� ��� �������
    private float timer = 0f;
    private bool arrived = false;

    void Start()
    {
        // ��������� �������: ����� � ������ (�� ��������� ������)
        leftStart = new Vector2(-800f, 0f);  // ����� ����� � �� ��������� �����
        rightStart = new Vector2(800f, 0f); // ������ ����� � �� ��������� ������

        // �����, ���� ��� ������ �����������
        centerTarget = new Vector2(0f, 0f); // ����� �� ��� X, Y 0 ��� ��������

        // ������������� ��������� �������
        titleLeft.anchoredPosition = leftStart;
        titleRight.anchoredPosition = rightStart;
    }

    void Update()
    {
        if (!arrived)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / moveDuration);

            // ��� ������ ������: �������� � �����
            Vector2 leftPos = Vector2.Lerp(leftStart, centerTarget, t);
            titleLeft.anchoredPosition = leftPos;

            // ��� ������� ������: �������� � �����
            Vector2 rightPos = Vector2.Lerp(rightStart, centerTarget, t);
            titleRight.anchoredPosition = rightPos;

            // ���� ��� ������ �������� ������
            if (t >= 1f)
            {
                arrived = true;
            }
        }
        else
        {
            // ����� ���� ��� ��� ����������� � �������� �����/�����
            float angle = Mathf.Sin(Time.time * swingSpeed) * swingAngle;
            titleLeft.localRotation = Quaternion.Euler(0, 0, angle);
            titleRight.localRotation = Quaternion.Euler(0, 0, -angle);
        }
    }
}