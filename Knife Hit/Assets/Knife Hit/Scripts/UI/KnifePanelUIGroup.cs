using UnityEngine;

public class KnifePanelUIGroup : UIGroup
{
    [SerializeField] private KnifeShopUI knifeShopUI;
    public override void Show()
    {
        base.Show();
        knifeShopUI.PopulateShop();
    }
}
