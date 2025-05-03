using UnityEngine;

public class UIGroup : MonoBehaviour
{
    public virtual void Show()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    public virtual void Hide()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false); 
        }
    }
}
