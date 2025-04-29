using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class StateSwitchButton : StateSwitcher
{
    private void Start()
    {
        Button button = GetComponent<Button>();  
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);  
        }
    }

    private void OnButtonClick()
    {
        StateSwitch();
    }

}
