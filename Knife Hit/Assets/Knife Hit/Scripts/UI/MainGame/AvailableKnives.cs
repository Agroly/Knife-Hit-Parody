using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnifePanelUI : MonoBehaviour
{
    [SerializeField] private GameObject knifeIconPrefab; // один префаб Image
    [SerializeField] private Sprite lightKnifeSprite;    // светлая иконка
    [SerializeField] private Sprite darkKnifeSprite;     // тёмная иконка

    private List<Image> knifeIcons = new();

    public void Initialize(int totalKnives)
    {
        ClearIcons();

        for (int i = 0; i < totalKnives; i++)
        {
            GameObject iconObj = Instantiate(knifeIconPrefab, transform);
            Image iconImage = iconObj.GetComponent<Image>();
            iconImage.sprite = lightKnifeSprite;
            knifeIcons.Add(iconImage);
        }
    }

    public void UseKnife()
    {
        foreach (var icon in knifeIcons)
        {
            if (icon.sprite == lightKnifeSprite)
            {
                icon.sprite = darkKnifeSprite;
                break;
            }
        }
    }



    private void ClearIcons()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        knifeIcons.Clear();
    }
}
