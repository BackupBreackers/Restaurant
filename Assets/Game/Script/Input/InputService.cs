using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;

public class InputService : MonoBehaviour
{
    public static InputService Instance { get; private set; }

    public struct PlayerInputData
    {
        public Vector2 MoveDirection;
        public bool InteractPressed;
        public bool PickPlacePressed;
        public bool RandomSpawnFurniturePressed;
        public bool MoveFurniturePressed;
    }

    private Dictionary<int, PlayerInput> _playerComponents = new(); // <--- НОВОЕ: Для хранения ссылок на PlayerInput
    private Dictionary<int, PlayerInputData> _playerInputs = new();

    private Queue<int> _pendingPlayerIndices = new();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public Action OnPausePressed;

    public void RegisterPlayer(int playerIndex, PlayerInput playerInput) // <--- ИЗМЕНЕНО: Принимает PlayerInput
    {
        if (!_playerInputs.ContainsKey(playerIndex))
        {
            _playerInputs[playerIndex] = new PlayerInputData();
            _playerComponents[playerIndex] = playerInput; // <--- НОВОЕ: Сохраняем компонент
            _pendingPlayerIndices.Enqueue(playerIndex);
            Debug.Log($"Player {playerIndex} registered in InputService.");
        }
    }

    public void SwitchAllActionMapsTo(string mapName) // <--- НОВОЕ: Метод для переключения
    {
        Debug.Log($"Switching all players to Action Map: {mapName}");
        foreach (var playerInput in _playerComponents.Values)
        {
            if (playerInput != null)
            {
                playerInput.SwitchCurrentActionMap(mapName);
            }
        }
    }

    public bool TryGetPendingPlayerIndex(out int index)
    {
        if (_pendingPlayerIndices.Count > 0)
        {
            index = _pendingPlayerIndices.Dequeue();
            return true;
        }

        index = -1;
        return false;
    }

    public void UpdateState(int playerIndex, PlayerInputData newData)
    {
        if (!_playerInputs.ContainsKey(playerIndex))
        {
            var playerInput = _playerComponents[playerIndex];
            RegisterPlayer(playerIndex, playerInput);
        }

        var currentData = _playerInputs[playerIndex];
        if (newData.InteractPressed) currentData.InteractPressed = true;
        if (newData.PickPlacePressed) currentData.PickPlacePressed = true;
        if (newData.RandomSpawnFurniturePressed) currentData.RandomSpawnFurniturePressed = true;
        if (newData.MoveFurniturePressed) currentData.MoveFurniturePressed = true;
        currentData.MoveDirection = newData.MoveDirection;
        _playerInputs[playerIndex] = currentData;
    }

    public PlayerInputData GetPlayerInputState(int playerIndex)
    {
        if (_playerInputs.TryGetValue(playerIndex, out var state))
        {
            return state;
        }

        return new PlayerInputData();
    }

    public int CountActivePlayerIndices() => _playerInputs.Count;

    private void LateUpdate()
    {
        var keys = _playerInputs.Keys.ToList();
        foreach (var playerIndex in keys)
        {
            var state = _playerInputs[playerIndex];
            state.InteractPressed = false;
            state.PickPlacePressed = false;
            state.RandomSpawnFurniturePressed = false;
            state.MoveFurniturePressed = false;
            _playerInputs[playerIndex] = state;
        }
    }
}