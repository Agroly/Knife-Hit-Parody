using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class KnifeSkinView : MonoBehaviour
{
    [SerializeField] private Image knifeImage;
    [SerializeField] private Image bg;
    [SerializeField] private Material lockedOverlay;
    [SerializeField] private Color defaultBGColor = Color.white;
    [SerializeField] private Color lockedBGColor = Color.black;
    [SerializeField] private GameObject selectionFrame;

    private bool isUnlocked;
    private KnifeSkinData data;
    private Button button;
    public static event Action<KnifeSkinView> OnSelected;

    public void Setup(KnifeSkinData data, bool isUnlocked, bool isSelected)
    {
        this.isUnlocked = isUnlocked;
        knifeImage.sprite = data.sprite;
        this.data = data;

        if (!isUnlocked)
        {
            knifeImage.material = lockedOverlay;
            bg.color = lockedBGColor;
        }
        else
        {
            knifeImage.material = null;
            bg.color = defaultBGColor;
        }
        SetSelected(isSelected);

        button = GetComponent<Button>();

        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        SetSelected(true);

        OnSelected?.Invoke(this);
    }

    public void SetSelected(bool selected)
    {
        if (selectionFrame != null)
            selectionFrame.SetActive(selected);

        if (selected)
        {
            KnifeSkinManager.Instance.ChangeCurrentSelected(data);

            KnifeSkinManager.Instance.ShowBuyButton(data,isUnlocked);
        }
    }

    private void OnEnable()
    {
        OnSelected += HandleOtherSelected;
    }

    private void OnDisable()
    {
        OnSelected -= HandleOtherSelected;
    }

    private void HandleOtherSelected(KnifeSkinView selected)
    {
        if (selected != this)
        {
            SetSelected(false);
        }
    }
}
