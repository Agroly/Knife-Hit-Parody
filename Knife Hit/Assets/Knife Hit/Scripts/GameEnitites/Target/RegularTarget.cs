using UnityEngine;

public class RegularTarget : Target
{
    [SerializeField] private int minHits;
    [SerializeField] private int maxHits;
    public override void Init()
    {
        knifeHitsRequired = Random.Range(minHits, maxHits);
        base.Init();
    }
}
