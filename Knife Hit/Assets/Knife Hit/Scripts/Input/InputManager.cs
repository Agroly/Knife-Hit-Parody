using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction touchPressAction;
    private InputAction touchPositionAction;
    public static InputManager Instance { get; private set; }
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        touchPressAction = playerInput.actions["TouchPress"];
        touchPositionAction = playerInput.actions["TouchPosition"];

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public void ListenToTouch()
    {
        touchPressAction.performed += TouchPressed;
    }
    public void StopListening()
    {
        touchPressAction.performed -= TouchPressed;
    }
    private void TouchPressed(InputAction.CallbackContext context)
    {
        Vector2 screenPosition = touchPositionAction.ReadValue<Vector2>();

        if (IsPointerOverUI(screenPosition))
            return;

        StartCoroutine(LevelManager.Instance.ShootAndSpawn());
    }

    private bool IsPointerOverUI(Vector2 screenPosition)
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = screenPosition
        };

        var raycastResults = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, raycastResults);

        foreach (var result in raycastResults)
        {
            if (result.gameObject.GetComponent<Button>() != null)
                return true;
        }

        return false;
    }

}
