using UnityEngine;

public class KnifeShopUI : MonoBehaviour
{
    [SerializeField] private GameObject skinViewPrefab;
    [SerializeField] private Transform gridParent;

    public void PopulateShop()
    {
        KnifeSkinManager.Instance.Init();
        foreach (Transform child in gridParent)
            Destroy(child.gameObject);

        foreach (var data in KnifeSkinManager.Instance.GetAllSkins())
        {
            var view = Instantiate(skinViewPrefab, gridParent).GetComponent<KnifeSkinView>();
            bool isUnlocked = KnifeSkinManager.Instance.IsUnlocked(data.skin);
            bool isSelected = KnifeSkinManager.Instance.IsSelected(data.skin);
            view.Setup(data, isUnlocked, isSelected);
        }
    }
}
