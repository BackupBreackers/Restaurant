using UnityEngine;
using UnityEngine.InputSystem;

public class InputService : MonoBehaviour
{
    public Vector2 MoveDirection {get; private set;}
    
    public void OnMove(InputValue value)
    {
        MoveDirection = value.Get<Vector2>();
    }
}
