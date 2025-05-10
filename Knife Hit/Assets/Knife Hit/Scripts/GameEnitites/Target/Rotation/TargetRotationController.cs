using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;
public class TargetRotationController : MonoBehaviour
{
    public RotationProfile profile;

    private void Start()
    {
        Rotate(transform);
    }
    public void Rotate(Transform transform)
    {
        transform.DORotate(
            new Vector3(0, 0, profile.angle),
            Random.Range(profile.durationMin,profile.durationMax),
            profile.rotateMode
        )
        .SetEase(profile.ease)
        .SetLoops(profile.loopNumber, profile.loopType)
        .SetLink(transform.gameObject)
        .OnComplete(() => Rotate(transform));
        
    }


}
