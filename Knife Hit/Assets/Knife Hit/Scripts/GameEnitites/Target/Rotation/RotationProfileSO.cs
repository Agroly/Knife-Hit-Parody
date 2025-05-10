using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(menuName = "Target/Rotation")]
public class RotationProfile : ScriptableObject
{
    public float angle = 90f;
    public float durationMin = 1f;
    public float durationMax = 1f;
    public RotateMode rotateMode = RotateMode.LocalAxisAdd;
    public Ease ease = Ease.Linear;
    public LoopType loopType = LoopType.Restart;
    public int loopNumber = -1;
}