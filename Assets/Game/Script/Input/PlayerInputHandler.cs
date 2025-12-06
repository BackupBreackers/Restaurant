using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput _playerInput;
    
    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        InputService.Instance.RegisterPlayer(_playerInput.playerIndex);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        int index = _playerInput.playerIndex;
        
        var val = context.ReadValue<Vector2>();
        var state = InputService.Instance.GetPlayerInputState(index);
        state.MoveDirection = val;
        Debug.Log(state.MoveDirection);
        InputService.Instance.UpdateState(index, state);
    }
    
    public void OnPickPlace(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            int index = _playerInput.playerIndex;
            var state = InputService.Instance.GetPlayerInputState(index);
            state.PickPlacePressed = true;
            InputService.Instance.UpdateState(index, state);
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            int index = _playerInput.playerIndex;
            var state = InputService.Instance.GetPlayerInputState(index);
            state.InteractPressed = true;
            InputService.Instance.UpdateState(index, state);
        }
    }
}