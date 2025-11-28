using UnityEngine;
using UnityEngine.InputSystem;

public class InputService : MonoBehaviour
{
    public Vector2 MoveDirection { get; private set; }
    public bool InteractPressed { get; private set; }
    public bool PickPlacePressed { get; private set; }

    public void OnInteract(InputValue value)
    {
        if (value.isPressed)
        {
            InteractPressed = true;
        }
    }

    public void OnPickPlace(InputValue value)
    {
        if (value.isPressed)
        {
            PickPlacePressed = true;
        }
    }

    public void OnMove(InputValue value)
    {
        MoveDirection = value.Get<Vector2>();
    }

    public bool RandomSpawnFurniturePressed { get; private set; } // true только один кадр

    public void OnRandomSpawnFurniture(InputValue value)
    {
        if (value.isPressed)
        {
            RandomSpawnFurniturePressed = true;
        }
    }

    public bool MoveFurniturePressed { get; private set; } // true только один кадр

    public void OnMoveFurniture(InputValue value)
    {
        if (value.isPressed)
        {
            MoveFurniturePressed = true;
        }
    }

    private void LateUpdate()
    {
        InteractPressed = false; // сбрасываем каждый кадр
        PickPlacePressed = false;

        RandomSpawnFurniturePressed = false;
        MoveFurniturePressed = false;
    }
}