using UnityEngine;
[ExecuteAlways]
public class BossTarget : Target
{
    [SerializeField] private int bossKnifeHits = 10;
    [SerializeField] private string _name;

    private void Awake()
    {
        Init();   
    }
    public override void Init()
    {
        knifeHitsRequired = bossKnifeHits;
        base.Init();
    }
}
