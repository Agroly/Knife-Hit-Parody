using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction touchPressAction;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        touchPressAction = playerInput.actions["TouchPress"];
    }
    private void OnEnable()
    {
        touchPressAction.performed += TouchPressed;
    }
    private void TouchPressed(InputAction.CallbackContext context)
    {
        Debug.Log("Pressed");
    }
}
