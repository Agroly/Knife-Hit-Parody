using System.Collections.Generic;
using System.Linq;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class KnifeSkinManager : MonoBehaviour
{
    public static KnifeSkinManager Instance;

    [SerializeField] private List<KnifeSkinData> skinDataList;
    [SerializeField] private Image selectedImage;
    [SerializeField] private Image MainMenuKnife;
    [SerializeField] private GameObject buyButton;
    [SerializeField] private Material LockedPreview;
    [SerializeField] private KnifeShopUI shopUI;
    

    private HashSet<KnifeSkinData> unlockedSkins = new HashSet<KnifeSkinData>();

    private KnifeSkinData selectedKnife;
    private KnifeSkinData currentSelectedKnife;
    

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        Init();
    }
    public void Init()
    {
        LoadUnlockedSkins();
        LoadSelectedSkin();
    }
    private void LoadUnlockedSkins()
    {
        unlockedSkins.Clear();

        string saved = PlayerPrefs.GetString("UnlockedAppleSkins", "");
        foreach (var str in saved.Split(','))
        {
            if (System.Enum.TryParse(str, out KnifeSkinsApples skin))
                unlockedSkins.Add(GetDataBySkin(skin));
        }

        if (!unlockedSkins.Contains(GetDataBySkin(KnifeSkinsApples.Default)))
        {
            unlockedSkins.Add(GetDataBySkin(KnifeSkinsApples.Default));
            SaveUnlockedSkins();
        }
    }
    private KnifeSkinData GetDataBySkin(KnifeSkinsApples skin)
    {
        return skinDataList.Find(data => data.skin == skin);
    }

    private void SaveUnlockedSkins()
    {
        string save = string.Join(",", unlockedSkins);
        PlayerPrefs.SetString("UnlockedAppleSkins", save);
        PlayerPrefs.Save();
    }
    private void LoadSelectedSkin()
    {
        string saved = PlayerPrefs.GetString("SelectedSkin");
        Debug.Log(saved);
        if (System.Enum.TryParse(saved, out KnifeSkinsApples skin))
        {
            selectedKnife = skinDataList.Find(data => data.skin == skin);
        }
        else
        {
            selectedKnife = skinDataList.Find(data => data.skin == KnifeSkinsApples.Default);
        }
        ChangeCurrentSelected(selectedKnife);
    }
    public void SaveSelectedSkin(KnifeSkinData data)
    {
        string save = data.skin.ToString();
        selectedKnife = data;
        PlayerPrefs.SetString("SelectedSkin", save);
        PlayerPrefs.Save();
    }
    //private void ChangeMainMenuImage(Sprite sprite)
    //{
    //    MainMenuKnife.sprite = sprite;
    //}
    public void ChangeCurrentSelected(KnifeSkinData data)
    {
        selectedImage.sprite = data.sprite;
        if (!IsUnlocked(data.skin)) selectedImage.material = LockedPreview;
        else
        {
            selectedImage.material = null;
            MainMenuKnife.sprite = data.sprite;
            SaveSelectedSkin(data);
        }
        currentSelectedKnife = data;
    }
    public void ShowBuyButton(KnifeSkinData data, bool isUnlocked) 
    {
        buyButton.gameObject.SetActive(!isUnlocked);
    }
    public bool IsUnlocked(KnifeSkinsApples skin) => unlockedSkins.Contains(GetDataBySkin(skin));

    public bool IsSelected(KnifeSkinsApples skin) => selectedKnife.skin == skin;

    public Sprite GetSelectedSprite() => selectedKnife.sprite;

    public void UnlockRandom()
    {
        if (!ApplesManager.Instance.RemoveApples(500)) return; 
            // Получаем все скины, которые ещё не разблокированы
          List<KnifeSkinData> availableSkins = skinDataList.Where(data => !unlockedSkins.Contains(data)).ToList();

        // Если есть доступные скины для разблокировки
        if (availableSkins.Count > 0)
        {
            // Выбираем случайный скин из оставшихся
            KnifeSkinData randomSkinData = availableSkins[Random.Range(0, availableSkins.Count)];

            // Разблокируем скин
            unlockedSkins.Add(randomSkinData);

            // Обновляем UI
            ChangeCurrentSelected(randomSkinData);
            SaveUnlockedSkins(); // Сохраняем изменения
        }
        else
        {
            Debug.Log("Нет доступных скинов для разблокировки.");
        }
    }


    public List<KnifeSkinData> GetAllSkins() => skinDataList;

    public void UnlockSkin(KnifeSkinData data)
    {
        // Если скин ещё не разблокирован, то добавляем его в список
        if (!unlockedSkins.Contains(GetDataBySkin(data.skin)))
        {
            unlockedSkins.Add(GetDataBySkin(data.skin));
            ChangeCurrentSelected(data);
            SaveUnlockedSkins();  // Сохраняем изменения
            SaveSelectedSkin(data);
            shopUI.PopulateShop();
        }
    }

    // Метод для использования кнопки покупки
    public void OnBuyButtonClicked()
    {
        var skinData = currentSelectedKnife;
        if (ApplesManager.Instance.RemoveApples(skinData.appleCost))
        {
            UnlockSkin(skinData);  // Разблокируем скин
            ShowBuyButton(skinData, true);  // Скрываем кнопку покупки
        }
    }
}
