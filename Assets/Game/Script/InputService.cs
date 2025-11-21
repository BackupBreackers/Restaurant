using UnityEngine;
using UnityEngine.InputSystem;

public class InputService : MonoBehaviour
{
    public Vector2 MoveDirection {get; private set;}
    
    public void OnMove(InputValue value)
    {
        MoveDirection = value.Get<Vector2>();
    }
    public bool InteractPressed { get; private set; } // true только один кадр

    public void OnInteract(InputValue value)
    {
        if (value.isPressed)
        {
            InteractPressed = true;
        }
    }

    private void LateUpdate()
    {
        InteractPressed = false; // сбрасываем каждый кадр
    }
}
