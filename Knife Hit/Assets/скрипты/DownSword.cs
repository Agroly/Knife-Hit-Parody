using UnityEngine;

public class SpringButtonEffect : MonoBehaviour
{
    public RectTransform target; // Твоя кнопка
    public float duration = 0.6f;

    private AnimationCurve springCurve;

    void Awake()
    {
        if (target == null)
            target = GetComponent<RectTransform>();

        // Спрятать кнопку в начале
        target.localScale = Vector3.zero;

        // Создаём пружинную кривую программно
        springCurve = new AnimationCurve(
            new Keyframe(0f, 0f),
            new Keyframe(0.5f, 1.3f),
            new Keyframe(0.75f, 0.9f),
            new Keyframe(1f, 1f)
        );

        // Немного "упругости"
        for (int i = 0; i < springCurve.length; i++)
        {
            springCurve.SmoothTangents(i, 0.5f);
        }
    }

    void Start()
    {
        StartCoroutine(PlaySpringEffect());
    }

    System.Collections.IEnumerator PlaySpringEffect()
    {
        float time = 0f;

        while (time < duration)
        {
            float t = time / duration;
            float scale = springCurve.Evaluate(t);
            target.localScale = new Vector3(scale, scale, scale);
            time += Time.deltaTime;
            yield return null;
        }

        // Убедиться, что в конце точно масштаб = 1
        target.localScale = Vector3.one;
    }
}