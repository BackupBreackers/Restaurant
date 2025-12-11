using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    private Dictionary<int, PlayerInputData> _playerInputs = new();
    
    // Очередь для передачи индексов от Unity к ECS при спавне
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

    // Вызываем это из PlayerInputHandler при старте
    public void RegisterPlayer(int playerIndex)
    {
        if (!_playerInputs.ContainsKey(playerIndex))
        {
            _playerInputs[playerIndex] = new PlayerInputData();
            // Добавляем индекс в очередь на инициализацию для ECS
            _pendingPlayerIndices.Enqueue(playerIndex);
            Debug.Log($"Player {playerIndex} registered in InputService.");
        }
    }
    
    // ECS система будет забирать индекс отсюда
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
        // Убрали создание ключа здесь, так как RegisterPlayer это уже сделал.
        // Но для надежности можно оставить проверку.
        if (!_playerInputs.ContainsKey(playerIndex))
        {
            RegisterPlayer(playerIndex); 
        }
        
        var currentData = _playerInputs[playerIndex];
        // ... (остальная логика без изменений)
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
        // Убрал лог, чтобы не спамил, если индекс еще не инициализирован
        return new PlayerInputData(); 
    }

    public int CountActivePlayerIndices() => _playerInputs.Count;

    private void LateUpdate()
    {
        // Тут без изменений, сброс флагов
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