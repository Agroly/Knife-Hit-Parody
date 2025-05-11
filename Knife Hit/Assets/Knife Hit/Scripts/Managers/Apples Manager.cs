using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ApplesManager : MonoBehaviour
{
    public static ApplesManager Instance { get; private set; }

    private int currentApples;

    [SerializeField] private List<Text> appleTexts;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        currentApples = PlayerPrefs.GetInt("apples");
        UpdateApples();
    }
    public void AddApples(int increment)
    {
        currentApples += 1000;
        UpdateApples();
    }
    public bool RemoveApples(int decrement)
    {
        if (currentApples - decrement >= 0)
        {
            currentApples -= decrement;
            UpdateApples();
            return true;
        }
        else return false;
    }
    private void UpdateApples()
    {
        foreach (Text text in appleTexts)
        {
            text.text = currentApples.ToString();
        }
        PlayerPrefs.SetInt("apples", currentApples);
    }
}
